using KerbalSimpitRefactored.Common;
using KerbalSimpitRefactored.Common.Enums;
using SimpitRefactored.Common.Core.Utilities;
using SimpitRefactored.Core;
using SimpitRefactored.Core.Peers;
using System;
using WindowsInput;
using WindowsInput.Native;

namespace SimpitRefactored.Unity.Common.Controllers
{
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    public class KeyboardController : SimpitBehaviour,
        ISimpitMessageSubscriber<KerbalSimpit.Messages.Commands.KeyboardEmulator>
    {
        private InputSimulator _input = new InputSimulator();

        public void Process(SimpitPeer peer, ISimpitMessage<KerbalSimpit.Messages.Commands.KeyboardEmulator> message)
        {
            try
            {
                int key32 = message.Data.Key; //To cast it in the enum, we need a Int32 but only a Int16 is sent

                if (Enum.IsDefined(typeof(VirtualKeyCode), key32))
                {
                    VirtualKeyCode key = (VirtualKeyCode)key32;
                    if ((message.Data.Modifier & KeyboardEmulatorModifierFlags.ALT_MOD) != 0)
                    {
                        _input.Keyboard.KeyDown(VirtualKeyCode.MENU);
                    }

                    if ((message.Data.Modifier & KeyboardEmulatorModifierFlags.CTRL_MOD) != 0)
                    {
                        _input.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
                    }

                    if ((message.Data.Modifier & KeyboardEmulatorModifierFlags.SHIFT_MOD) != 0)
                    {
                        // Use LSHIFT instead of SHIFT since some function (like SHIFT+Tab to cycle through bodies in map view) only work with left shift.
                        // This requires a custom version of the WindowsInput library to properly handle it.
                        _input.Keyboard.KeyDown(VirtualKeyCode.LSHIFT);
                    }

                    if ((message.Data.Modifier & KeyboardEmulatorModifierFlags.KEY_DOWN_MOD) != 0)
                    {
                        this.logger.LogDebug("Simpit emulates key down of " + key);
                        _input.Keyboard.KeyDown(key);
                    }
                    else if ((message.Data.Modifier & KeyboardEmulatorModifierFlags.KEY_UP_MOD) != 0)
                    {
                        this.logger.LogDebug("Simpit emulates key up of " + key);
                        _input.Keyboard.KeyUp(key);
                    }
                    else
                    {
                        this.logger.LogDebug("Simpit emulates keypress of " + key);
                        _input.Keyboard.KeyPress(key);
                    }

                    if ((message.Data.Modifier & KeyboardEmulatorModifierFlags.ALT_MOD) != 0)
                    {
                        _input.Keyboard.KeyUp(VirtualKeyCode.MENU);
                    }

                    if ((message.Data.Modifier & KeyboardEmulatorModifierFlags.CTRL_MOD) != 0)
                    {
                        _input.Keyboard.KeyUp(VirtualKeyCode.CONTROL);
                    }

                    if ((message.Data.Modifier & KeyboardEmulatorModifierFlags.SHIFT_MOD) != 0)
                    {
                        _input.Keyboard.KeyUp(VirtualKeyCode.LSHIFT);
                    }
                }
                else
                {
                    this.logger.LogDebug("I received a message to emulate a keypress of key {0} but I do not recognize it. I ignore it.", key32);
                }

            }
            catch (DllNotFoundException exception)
            {
                this.logger.LogWarning("Simpit : I received a message to emulate a keypress. This is currently only available on Windows. I ignore it.");
            }
        }
    }
}
