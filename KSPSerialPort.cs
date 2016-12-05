using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

using KSP.IO;
using UnityEngine;

using Psimax.IO.Ports;

public class KSPSerialPort
{
    public string PortName;
    private int BaudRate;
    public  int ID;

    private SerialPort Port;
    // Method signature of callback functions must be:
    // callback(int idx, int type, object data)
    private Func<int, byte, object, bool> callback;

    // Header bytes are alternating ones and zeroes, with the exception
    // of encoding the protocol version in the final four bytes.
    private readonly byte[] PacketHeader = { 0xAA, 0x50 };

    // Packet buffer related fields
    // This is *total* packet size, including all headers.
    private const int MaxPacketSize = 32;
    // Buffer for sending outbound packets
    private byte[] OutboundPacketBuffer;
    private enum ReceiveStates: byte {
        HEADER1, // Waiting for first header byte
        HEADER2, // Waiting for second header byte
        SIZE,    // Waiting for payload size
        TYPE,    // Waiting for packet type
        PAYLOAD  // Waiting for payload packets
    }
    // Serial worker uses these to bufferinbound data
    private ReceiveStates CurrentState;
    private byte CurrentPayloadSize;
    private byte CurrentPayloadType;
    private byte CurrentBytesRead;
    private byte[] PayloadBuffer = new byte[255];
    // Semaphore to indicate whether the reader worker should do work
    private volatile bool DoSerialRead;
    private Thread SerialThread;

    // Constructors:
    // pn: port number
    // br: baud rate
    // idx: a unique identifier for this port
    public KSPSerialPort(string pn, int br): this(pn, br, 37, false)
    {
    }
    public KSPSerialPort(string pn, int br, int idx): this(pn, br, idx, false)
    {
    }
    public KSPSerialPort(string pn, int br, int idx, bool vb)
    {
        PortName = pn;
        BaudRate = br;
        ID = idx;

        DoSerialRead = false;
        // Note that we initialise the packet buffer once, and reuse it.
        // I don't know if that's acceptable C# or not.
        // But I hope it's faster.
        OutboundPacketBuffer = new byte[MaxPacketSize];
        Array.Copy(PacketHeader, OutboundPacketBuffer, PacketHeader.Length);

        Port = new SerialPort(PortName, BaudRate, Parity.None,
                              8, StopBits.One);
    }

    // Open the serial port
    public bool open() {
        if (!Port.IsOpen)
        {
            try
            {
                Port.Open();
                SerialThread = new Thread(ReaderWorker);;
                SerialThread.Start();
                while (!SerialThread.IsAlive);
            }
            catch (Exception e)
            {
                Debug.Log(String.Format("KerbalSimPit: Error opening serial port {0}: {1}", PortName, e.Message));
            }
        }
        return Port.IsOpen;
    }

    // Close the serial port
    public void close() {
        if (Port.IsOpen)
        {
            DoSerialRead = false;
            Thread.Sleep(500);
            Port.Close();
        }
    }

    // Register a function to handle inbound data
    public void registerPacketHandler(Func<int, byte, object, bool> packetHandler)
    {
        callback = packetHandler;
    }

    // Send a KerbalSimPit packet
    public void sendPacket(byte Type, object Data)
    {
        // Note that header sizes are hardcoded here:
        // packet[0] = first byte of header
        // packet[1] = second byte of header
        // packet[2] = payload size
        // packet[3] = packet type
        // packet[4-x] = packet payload
        // TODO: Right now calling sendPacket with a 2 byte array as data
        // results in buf being 30 bytes. Figure out why.
        byte[] buf = ObjectToByteArray(Data);
        byte PayloadSize = (byte)Math.Min(buf.Length, (MaxPacketSize-4));
        byte PacketSize = (byte)(PayloadSize + 4);
        OutboundPacketBuffer[2] = PacketSize;
        OutboundPacketBuffer[3] = Type;
        Array.Copy(buf, 0, OutboundPacketBuffer, 4, PayloadSize);
        if (Port.IsOpen)
        {
            Port.Write(OutboundPacketBuffer, 0, PacketSize);
        }
    }

    // Send arbitrary data. Shouldn't be used.
    private void sendData(object data)
    {
        byte[] buf = ObjectToByteArray(data);
        if (buf != null && Port.IsOpen)
        {
            Port.Write(buf, 0, buf.Length);
        }
    }

    // Convert the given object to an array of bytes
    private byte[] ObjectToByteArray(object obj)
    {
        if (obj == null)
        {
            return null;
        }
        BinaryFormatter bf = new BinaryFormatter();
        using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
        {
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }
    }

    // This method spawns a new thread to read data from the serial connection
    private void ReaderWorker()
    {
        byte[] buffer = new byte[MaxPacketSize];
        Action SerialRead = null;
        SerialRead = delegate {
            try
            {
                Port.BaseStream.BeginRead(buffer, 0, buffer.Length, delegate(IAsyncResult ar) {
                        try
                        {
                            int actualLength = Port.BaseStream.EndRead(ar);
                            byte[] received = new byte[actualLength];
                            Buffer.BlockCopy(buffer, 0, received, 0, actualLength);
                            ReceivedDataEvent(received, actualLength);
                        }
                        catch(System.IO.IOException exc)
                        {
                            Debug.Log(String.Format("KerbalSimPit: IOException in serial worker for {0}: {1}", PortName, exc.ToString()));
                        }
                    }, null);
            }
            catch (InvalidOperationException)
            {
                Debug.Log(String.Format("KerbalSimPit: Trying to read port {0} that isn't open, sleeping", PortName));
                Thread.Sleep(500);
            }
        };
        DoSerialRead = true;
        Debug.Log(String.Format("KerbalSimPit: Starting read thread for port {0}", PortName));
        while (DoSerialRead)
        {
            SerialRead();
        }
    }

    // Handle data read in worker thread
    private void ReceivedDataEvent(byte[] ReadBuffer, int BufferLength)
    {
        for (int x=0; x<BufferLength; x++)
        {
            switch(CurrentState)
            {
                case ReceiveStates.HEADER1:
                    if (ReadBuffer[x] == PacketHeader[0])
                    {
                        CurrentState = ReceiveStates.HEADER2;
                    }
                    break;
                case ReceiveStates.HEADER2:
                    if (ReadBuffer[x] == PacketHeader[1])
                    {
                        CurrentState = ReceiveStates.SIZE;
                    } else
                    {
                        CurrentState = ReceiveStates.HEADER1;
                    }
                    break;
                case ReceiveStates.SIZE:
                    CurrentPayloadSize = ReadBuffer[x];
                    CurrentState = ReceiveStates.TYPE;
                    break;
                case ReceiveStates.TYPE:
                    CurrentPayloadType = ReadBuffer[x];
                    CurrentBytesRead = 0;
                    CurrentState = ReceiveStates.PAYLOAD;
                    break;
                case ReceiveStates.PAYLOAD:
                    PayloadBuffer[CurrentBytesRead] = ReadBuffer[x];
                    CurrentBytesRead++;
                    if (CurrentBytesRead == CurrentPayloadSize)
                    {
                        if (callback != null)
                        {
                            callback(ID, CurrentPayloadType, PayloadBuffer);
                        }
                        CurrentState = ReceiveStates.HEADER1;
                    }
                    break;
            }
        }
    }       
}
