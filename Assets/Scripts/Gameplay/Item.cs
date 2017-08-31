using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    [SerializeField]
    int healAmount;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")){
            gameObject.SetActive(false);
            AppEvents.Instance.GamePlay.Fire(GamePlayEventBroadcaster.EventName.OnPicked,healAmount);

        }
    }
}
