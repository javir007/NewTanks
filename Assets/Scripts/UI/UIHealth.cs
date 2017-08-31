using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealth : MonoBehaviour,IEventListener {
    
    private Slider health;
    private int UI_Id;
    private Transform target;
    private Enemy enemy;


    void Start(){
        health = GetComponentInChildren<Slider>();
		if (target != null)
			transform.position = new Vector3(target.position.x, transform.position.y, target.position.z);
    }

    public static UIHealth Spawn(Canvas prefab, Vector3 position, Quaternion rotation)
	{
        Canvas go = Instantiate(prefab, position, rotation);
		return go.GetComponent<UIHealth>();
	}

    public void Set(IAvatar data){
        UI_Id = data.Id;
        target = data.CachedTransform;
    }

    private void Update(){
        if(target!=null)
            transform.position = new Vector3(target.position.x, transform.position.y, target.position.z);
    }

    public void OnEvent(string eventName, object content){
        
        if (eventName == EnemyEventBroadcaster.EventName.OnEnemyHurt){
            enemy = (Enemy)content;
            if(enemy.Id == UI_Id){
                health.value = enemy.Health;
            }
		}
		else if (eventName == EnemyEventBroadcaster.EventName.OnEnemyDied)		{
            enemy = (Enemy)content;
			if (enemy.Id == UI_Id)
                Destroy(gameObject);
		}
        else if (eventName == PlayerEventBroadcaster.EventName.OnPlayerHurt){
            health.value = (int)content;
        }else if(eventName == PlayerEventBroadcaster.EventName.OnPlayerHeal){
            health.value = (int)content;
        }
    }
}
