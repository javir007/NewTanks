using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MatchController : MonoSingleton<MatchController>,IEventListener
{
	
	public GameObject Player;

    [SerializeField]
    GameObject EnemyPrefab;


	[SerializeField]
    List<Transform> SpawnPoints;

	[SerializeField]
	float timeBewtweenWaves;

    [SerializeField]
    List<AvatarData> enemies;


    private int FinalWave = 10;
	private bool finishMatch;
	private int Waves = 1;
	private int enemyDeads = 0;

    public bool FinishMatch{get{return finishMatch;}}

    void Start()
    {
        StartCoroutine(StartGame());
        Waves = 1;
    }

	private void Update()
	{
		if (enemyDeads == Waves)
		{
			enemyDeads = 0;
			Waves++;
			StartCoroutine(SpawnEnemies());
		}

        if (Waves == FinalWave){
            AppEvents.Instance.GamePlay.Fire(GamePlayEventBroadcaster.EventName.Win);
            finishMatch = true;
            StartCoroutine(LoadScene());
        }
       
	}

	void Spawn()
	{
        
		foreach (AvatarData enemy in enemies)
		{
            int index = Random.Range(0, SpawnPoints.Count);
            IAvatar avatar =  Enemy.Spawn(EnemyPrefab, SpawnPoints[index].position);
		    avatar.SetAvatar(enemy);
		    MatchData.Instance.Enemies.Add(avatar);
            AppEvents.Instance.Enemy.Fire(EnemyEventBroadcaster.EventName.OnEnemySpawn,avatar);
		}
	}

	IEnumerator SpawnEnemies()
	{
        AppEvents.Instance.GamePlay.Fire(GamePlayEventBroadcaster.EventName.OnWaveStartd, Waves);
		yield return new WaitForSeconds(timeBewtweenWaves);
		int count = 0;
		while (count < Waves)
		{
			Spawn();
			count++;
		}
	}

    IEnumerator StartGame(){
		yield return new WaitForSeconds(1f);
        AppEvents.Instance.GamePlay.Fire(GamePlayEventBroadcaster.EventName.OnWaveStartd,1);
        Spawn();
    }

	public void OnEvent(string eventName, object content)
	{
		if (eventName == EnemyEventBroadcaster.EventName.OnEnemyDied)
		{
			enemyDeads++;
		}
        else if(eventName == PlayerEventBroadcaster.EventName.OnPlayerDie){
            finishMatch = true;
            StartCoroutine(LoadScene());
        }
	}

    IEnumerator LoadScene(){
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("MainScene");
    }



}
