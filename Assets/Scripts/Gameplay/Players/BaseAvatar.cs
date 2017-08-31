using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAvatar : MonoBehaviour, IAvatar
{
    public abstract AvatarTypes AvatarType { get; }
    public Transform CachedTransform { get; private set; }
    public int Health { get; protected set; }
    public int Id { get; protected set; }
    public string Nickname { get; protected set; }
    protected IShooter shooter;
    List<IShootable> bodyParts = new List<IShootable>();

    public bool isAlive;

    protected virtual void Awake()
    {
        isAlive = true;
        CachedTransform = transform;
        shooter = GetComponent<IShooter>();
        if (shooter == null)
            Debug.LogError("No IShooter component found in " + gameObject.name);

        var shootables = GetComponentsInChildren<IShootable>();
        foreach (IShootable shootable in shootables)
        {
            shootable.OnDamage = OnApplyDamage;
        }
    }

    protected abstract void Die();
    public virtual void SetAvatar(AvatarData data)
    {
        Nickname = data.NickName;
        Health = data.InitialHealth;
        data.Id++;
        Id = data.Id;
    }

    protected virtual void OnApplyDamage(IShooter attacker)
    {
        Health -= attacker.DamageRate;

        if (Health <= 0)
            Die();
    }

    public virtual void Fire()
    {
        shooter.Fire();
    }
}
