using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayEventBroadcaster : IEventBroadcaster
{
	public class EventName
	{
		public static readonly string OnWaveStartd = "new_wave";
        public static readonly string Win = "win";
        public static readonly string OnPicked = "picked";
	}

    public EventGroup Group { get { return EventGroup.GamePlay; } }
	public Action<string, object> OnEventFired { get; set; }

    public void Fire(string eventName, object content = null)
	{
		if (OnEventFired != null)
			OnEventFired(eventName, content);
	}
}