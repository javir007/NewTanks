using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventBroadcaster : IEventBroadcaster
{
    public class EventName
    {
        public static readonly string OnPlayerHurt = "player_hurt";
        public static readonly string OnPlayerHeal = "player_heal";
        public static readonly string OnPlayerDie = "player_dies";
        public static readonly string OnPlayerFire = "player_fire";
    }

    public EventGroup Group { get { return EventGroup.Player; } }
    public Action<string, object> OnEventFired { get; set; }

    public void Fire(string eventName, object content = null)
    {
        if (OnEventFired != null)
            OnEventFired(eventName, content);
    }
}

