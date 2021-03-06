﻿using System;
using homeControl.Configuration.Switches;
using homeControl.Peripherals;

namespace homeControl.Application
{
    internal sealed class SwitchControllerConsoleEmulator : ISwitchController
    {
        public bool CanHandleSwitch(SwitchId switchId)
        {
            return true;
        }

        public void TurnOn(SwitchId switchId)
        {
            Guard.DebugAssertArgumentNotNull(switchId, nameof(switchId));
            Console.WriteLine($"{switchId}: TurnOn");
        }

        public void TurnOff(SwitchId switchId)
        {
            Guard.DebugAssertArgumentNotNull(switchId, nameof(switchId));
            Console.WriteLine($"{switchId}: TurnOff");
        }
    }
}