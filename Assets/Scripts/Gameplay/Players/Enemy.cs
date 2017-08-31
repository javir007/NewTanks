using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : BaseAvatar {
    
    public override AvatarTypes AvatarType { get { return AvatarTypes.AI; } }

    Transform target;

    [SerializeField]
    float range = 3f;

    [SerializeField]
    float timeBetweenAttacks = 1f;
    private NavMeshAgent nav;
    private bool playerInRange;


    public static IAvatar Spawn(GameObject prefab, Vector3 position)
    {
        GameObject go = Instantiate(prefab, position, Quaternion.identity);
        return go.GetComponent<IAvatar>();
    }


    protected override void OnApplyDamage(IShooter attacker){
        base.OnApplyDamage(attacker);
        AppEvents.Instance.Enemy.Fire(EnemyEventBroadcaster.EventName.OnEnemyHurt, this);
    }
    protected override void Die(){
        isAlive = false;
        AppEvents.Instance.Enemy.Fire(EnemyEventBroadcaster.EventName.OnEnemyDied,this);
        Destroy(gameObject);
    }

    void Start(){
        nav = GetComponent<NavMeshAgent>();
        StartCoroutine(Attack());
        Health = 100;
        target = MatchController.Instance.Player.transform;
    }

    void Update(){
        if ((Health > 0 && isAlive) && !MatchController.Instance.FinishMatch){
            nav.isStopped = false;
            nav.SetDestination(target.position);
        }
        else{
            nav.isStopped = true;
        }

        if (Vector3.Distance(transform.position, target.transform.position) < range && isAlive){
            playerInRange = true;
            RotateTowards(target.transform);
        }
        else{
            playerInRange = false;
        }
    }

    void RotateTowards(Transform player){
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
    }

    private IEnumerator Attack(){
        if ((playerInRange && isAlive) && !MatchController.Instance.FinishMatch)		{
            Fire();
            AppEvents.Instance.Enemy.Fire(EnemyEventBroadcaster.EventName.OnEnemyFire);
			yield return new WaitForSeconds(timeBetweenAttacks);
		}

		yield return null;
		StartCoroutine(Attack());
	}
	
}
