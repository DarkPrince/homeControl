﻿using System;

namespace homeControl.Events.Switches
{
    public class TurnOnEvent : AbstractSwitchEvent
    {
        public TurnOnEvent(SwitchId switchId) : base(switchId)
        {
        }
    }
}