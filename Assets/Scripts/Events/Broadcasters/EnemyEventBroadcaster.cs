using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEventBroadcaster : IEventBroadcaster
{
    public class EventName
    {
        public static readonly string OnEnemyDied = "enemy_died";
        public static readonly string OnEnemyHurt = "enemy_hurt";
        public static readonly string OnEnemySpawn = "enemy_spawn";
        public static readonly string OnEnemyFire = "enemy_fire";
    }

    public EventGroup Group {  get { return EventGroup.Enemy;  } }
    public Action<string, object> OnEventFired { get; set; }

    public void Fire(string eventName, object content = null)
    {
        if (OnEventFired != null)
            OnEventFired(eventName, content);
    }
}
