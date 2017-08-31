using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPlayer : BaseAvatar, IEventListener
{
    public override AvatarTypes AvatarType {  get { return AvatarTypes.Player; } }


    private Rigidbody m_Rigidbody;
    InputData inputdata;


	void Start(){
		m_Rigidbody = GetComponent<Rigidbody>();
        Health = 100;

    }
    protected override void OnApplyDamage(IShooter attacker){
        base.OnApplyDamage(attacker);
        AppEvents.Instance.Player.Fire(PlayerEventBroadcaster.EventName.OnPlayerHurt,Health);
    }
    protected override void Die(){
        isAlive = false;
        AppEvents.Instance.Player.Fire(PlayerEventBroadcaster.EventName.OnPlayerDie);
    }
    void Healer(int healing){
        if(Health <= 70){
            Health += healing;
        }else{
            Health = 100;
        }
        AppEvents.Instance.Player.Fire(PlayerEventBroadcaster.EventName.OnPlayerHeal, Health);
    }

    void Update(){
       inputdata = UserInput.Instance.InputRequested;
        if (inputdata.Fire > 0 && isAlive){
            AppEvents.Instance.Player.Fire(PlayerEventBroadcaster.EventName.OnPlayerFire);
	        Fire();
		}
    }

    private void FixedUpdate(){
		inputdata = UserInput.Instance.InputRequested;
		if (isAlive)
		{
			Move(inputdata.Move.y, 12f);
			Turn(inputdata.Move.x, 180f);
		}
    }

    private void Move(float move, float speed){
        Vector3 movement = transform.forward * move * speed * Time.deltaTime;
		m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
	}


	private void Turn(float Turning, float speed){
        float turn = Turning * speed * Time.deltaTime;
	    Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
		m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
	}

    public void OnEvent(string eventName, object content){
        if(eventName== GamePlayEventBroadcaster.EventName.OnPicked){
            Healer((int)content);
        }
    }
}
