using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>, IEventListener
{

    [SerializeField]
    Canvas prefabUI;

	[SerializeField]
	Text message;

	[SerializeField]
    Image bg;


    public void OnEvent(string eventName, object content)
    {
        if (eventName == EnemyEventBroadcaster.EventName.OnEnemySpawn)
        {
            UIHealth EnemyUI = UIHealth.Spawn(prefabUI, prefabUI.transform.position, prefabUI.transform.rotation);
            IAvatar data = (IAvatar)content;
            EnemyUI.Set(data);
            EnemyUI.transform.SetParent(gameObject.transform);
        }
        else if (eventName == GamePlayEventBroadcaster.EventName.OnWaveStartd)
        {
            message.text = " WAVE " + (int)content + " !!!!";
            StartCoroutine(FadeText(0));
        }
        else if (eventName == PlayerEventBroadcaster.EventName.OnPlayerDie)
        {
            StartCoroutine(FadeText(1));
            message.text = "¡¡ GAMEOVER !!";
            StartCoroutine(FadeScene(1));
        }
        else if (eventName == GamePlayEventBroadcaster.EventName.Win){
            message.text = "¡¡ WINNER !!";
            StartCoroutine(FadeScene(1));
        }
        ResetText();
    }


	IEnumerator FadeText(float targetAlpha)
	{
        yield return new WaitForSeconds(1f);
        while (message.color.a != targetAlpha)
		{
			Color c = message.color;
			c.a = Mathf.MoveTowards(c.a, targetAlpha, Time.deltaTime);
			message.color = c;
			yield return null;
		}
        message.text = "";
	}

	IEnumerator FadeScene(float targetAlpha)
	{
		while (bg.color.a != targetAlpha)
		{
			Color c = bg.color;
			c.a = Mathf.MoveTowards(c.a, targetAlpha, Time.deltaTime);
			bg.color = c;
			yield return null;
		}
	}

    private void ResetText()
    {
		Color c = message.color;
		c.a = 1;
		message.color = c;

    }
}