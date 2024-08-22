using KerbalSimpitRefactored.Common;
using KerbalSimpitRefactored.Common.Enums;
using KSP.UI.Screens;
using SimpitRefactored.Common.Core.Utilities;
using SimpitRefactored.Core;
using SimpitRefactored.Core.Peers;
using System.Collections.Generic;

namespace KerbalSimpitRefactored.Unity.KSP1.Controllers
{
    public partial class VesselController : ISimpitMessageSubscriber<KerbalSimpit.Messages.Commands.CustomActionGroupEnable>,
        ISimpitMessageSubscriber<KerbalSimpit.Messages.Commands.CustomActionGroupDisable>,
        ISimpitMessageSubscriber<KerbalSimpit.Messages.Commands.CustomActionGroupToggle>,
        ISimpitMessageSubscriber<KerbalSimpit.Messages.Commands.ActionGroupActivate>,
        ISimpitMessageSubscriber<KerbalSimpit.Messages.Commands.ActionGroupDeactivate>,
        ISimpitMessageSubscriber<KerbalSimpit.Messages.Commands.ActionGroupToggle>
    {
        public void Process(SimpitPeer peer, ISimpitMessage<KerbalSimpit.Messages.Commands.CustomActionGroupEnable> message)
        {
            foreach (int idx in message.Data.GroupIds.ToList<byte>())
            {
                FlightGlobals.ActiveVessel.ActionGroups.SetGroup(this.ActionGroupIDs[idx], true);
            }
        }

        public void Process(SimpitPeer peer, ISimpitMessage<KerbalSimpit.Messages.Commands.CustomActionGroupDisable> message)
        {
            foreach (int idx in message.Data.GroupIds.ToList<byte>())
            {
                FlightGlobals.ActiveVessel.ActionGroups.SetGroup(this.ActionGroupIDs[idx], false);
            }
        }

        public void Process(SimpitPeer peer, ISimpitMessage<KerbalSimpit.Messages.Commands.CustomActionGroupToggle> message)
        {
            foreach (int idx in message.Data.GroupIds.ToList<byte>())
            {
                if (idx == 0)
                {
                    continue;
                }

                FlightGlobals.ActiveVessel.ActionGroups.ToggleGroup(this.ActionGroupIDs[idx]);
            }
        }

        public void Process(SimpitPeer peer, ISimpitMessage<KerbalSimpit.Messages.Commands.ActionGroupActivate> message)
        {
            foreach (KSPActionGroup group in this.GetActionGroups(message.Data.Flags))
            {
                this.logger.LogInformation("Activating {0}", group);
                FlightGlobals.ActiveVessel.ActionGroups.SetGroup(group, true);

                if (group == KSPActionGroup.Stage)
                {
                    StageManager.ActivateNextStage();
                }
            }
        }

        public void Process(SimpitPeer peer, ISimpitMessage<KerbalSimpit.Messages.Commands.ActionGroupDeactivate> message)
        {
            foreach (KSPActionGroup group in this.GetActionGroups(message.Data.Flags))
            {
                this.logger.LogInformation("Deactivating {0}", group);
                FlightGlobals.ActiveVessel.ActionGroups.SetGroup(group, false);
            }
        }

        public void Process(SimpitPeer peer, ISimpitMessage<KerbalSimpit.Messages.Commands.ActionGroupToggle> message)
        {
            foreach (KSPActionGroup group in this.GetActionGroups(message.Data.Flags))
            {
                this.logger.LogInformation("Toggling {0}", group);
                FlightGlobals.ActiveVessel.ActionGroups.ToggleGroup(group);

                if (group == KSPActionGroup.Stage)
                {
                    StageManager.ActivateNextStage();
                }
            }
        }

        private IEnumerable<KSPActionGroup> GetActionGroups(ActionGroupFlags flgas)
        {
            if (flgas.HasFlag(ActionGroupFlags.Stage))
            {
                yield return KSPActionGroup.Stage;
            }

            if (flgas.HasFlag(ActionGroupFlags.Gear))
            {
                yield return KSPActionGroup.Gear;
            }

            if (flgas.HasFlag(ActionGroupFlags.Light))
            {
                yield return KSPActionGroup.Light;
            }

            if (flgas.HasFlag(ActionGroupFlags.RCS))
            {
                yield return KSPActionGroup.RCS;
            }

            if (flgas.HasFlag(ActionGroupFlags.SAS))
            {
                yield return KSPActionGroup.SAS;
            }

            if (flgas.HasFlag(ActionGroupFlags.Brakes))
            {
                yield return KSPActionGroup.Brakes;
            }

            if (flgas.HasFlag(ActionGroupFlags.Abort))
            {
                yield return KSPActionGroup.Abort;
            }
        }
    }
}
