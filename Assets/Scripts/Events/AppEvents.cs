using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppEvents 
{
    public static AppEvents Instance = new AppEvents();
    public Dictionary<EventGroup, IEventBroadcaster> Broadcasters;

    public PlayerEventBroadcaster Player = new PlayerEventBroadcaster();
    public EnemyEventBroadcaster Enemy = new EnemyEventBroadcaster();
    public GamePlayEventBroadcaster GamePlay = new GamePlayEventBroadcaster();


    public AppEvents()
    {
        Broadcasters = new Dictionary<EventGroup, IEventBroadcaster>();
        Broadcasters.Add(EventGroup.Player, Player);
        Broadcasters.Add(EventGroup.Enemy, Enemy);
        Broadcasters.Add(EventGroup.GamePlay, GamePlay);
    }
}
