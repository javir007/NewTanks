using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>, IEventListener {


	[SerializeField] AudioSource m_MovementAudio;  
    [SerializeField] AudioSource m_ShootAudio;
	[SerializeField] AudioClip m_EngineIdling;            
	[SerializeField] AudioClip m_EngineDriving;
    [SerializeField] AudioClip m_Explosion;
    [SerializeField] AudioClip m_Fire;
    [SerializeField] AudioClip m_item;


	InputData inputdata;

    public void OnEvent(string eventName, object content)
    {
        if (eventName == PlayerEventBroadcaster.EventName.OnPlayerHurt || eventName == EnemyEventBroadcaster.EventName.OnEnemyHurt)
        {
            m_ShootAudio.PlayOneShot(m_Explosion);
        }
        else if (eventName == EnemyEventBroadcaster.EventName.OnEnemyFire || eventName == PlayerEventBroadcaster.EventName.OnPlayerFire)
        {
            m_ShootAudio.PlayOneShot(m_Fire);
        }
        else if (eventName == PlayerEventBroadcaster.EventName.OnPlayerHeal)
		{
            m_ShootAudio.PlayOneShot(m_item);
		}
    }	

    private void Update()
    {
        EngineAudio();
    }

    private void EngineAudio()
	{
        inputdata = UserInput.Instance.InputRequested;
		// If there is no input (the tank is stationary)...
		if (Mathf.Abs(inputdata.Move.x) < 0.1f && Mathf.Abs(inputdata.Move.y) < 0.1f)
		{
			// ... and if the audio source is currently playing the driving clip...
			if (m_MovementAudio.clip == m_EngineDriving)
			{
				// ... change the clip to idling and play it.
				m_MovementAudio.clip = m_EngineIdling;
				
				m_MovementAudio.Play();
			}
		}
		else
		{
			// Otherwise if the tank is moving and if the idling clip is currently playing...
			if (m_MovementAudio.clip == m_EngineIdling)
			{
				// ... change the clip to driving and play.
				m_MovementAudio.clip = m_EngineDriving;
				m_MovementAudio.Play();
			}
		}
	}
}
