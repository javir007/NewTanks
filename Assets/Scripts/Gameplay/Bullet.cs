using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody r;
    IShooter shooter;
    [SerializeField]
    ParticleSystem explosion;

    private void Awake(){
        r = GetComponent<Rigidbody>();

    }

    public void Fire(IShooter shooterFiring){
        shooter = shooterFiring;
        r.AddForce(transform.forward * 800f);
    }

    void OnCollisionEnter(Collision collision){
       
        if(collision.gameObject != null)
        {
            var shootable = collision.gameObject.GetComponent<IShootable>();
            if (shootable != null){
				SetParticles(explosion, collision.gameObject.transform);
                shootable.ApplyDamage(shooter);
            }
             Destroy(gameObject);
        }
    }

    void SetParticles(ParticleSystem prefab, Transform transform){
        ParticleSystem go = Instantiate(prefab, transform.position, Quaternion.identity);
        go.transform.parent = null;
		explosion.Play();
		ParticleSystem.MainModule mainModule = explosion.main;
        Destroy(go.gameObject, mainModule.duration);
    }


}
