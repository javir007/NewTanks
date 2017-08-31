using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour,IEventListener {

    [SerializeField]
    GameObject itemPrefab;

    [SerializeField]
    List<Transform> itemPositions;


    private void Start()
    {
        itemPrefab.SetActive(false);
    }

    void Setup(){
        if(!itemPrefab.activeInHierarchy){
			int index = Random.Range(0, itemPositions.Count);
			itemPrefab.transform.position = itemPositions[index].transform.position;
			itemPrefab.SetActive(true);
        }
    }

    public void OnEvent(string eventName, object content)
    {
        if(eventName == GamePlayEventBroadcaster.EventName.OnWaveStartd){
            if((int)content % 2 == 0)
                Setup();
        }
    }
}
