using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter : MonoBehaviour, IShooter
{
    [SerializeField]
    Bullet bulletPrefab;
    [SerializeField]
    int damageRate;
    [SerializeField]
    Transform bulletSpawnPoint;

    public int DamageRate { get { return damageRate; } }
   
    [ContextMenu("Test Fire")]
    public void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab.gameObject, bulletSpawnPoint.position, bulletSpawnPoint.rotation); 
        Bullet projectile = bullet.GetComponent<Bullet>();
        projectile.Fire(this);
    }
}
