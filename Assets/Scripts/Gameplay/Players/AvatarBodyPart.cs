using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarBodyPart : MonoBehaviour, IShootable
{
    public Action<IShooter> OnDamage { get; set; }
   

    public void ApplyDamage(IShooter shooter)
    {
        if (OnDamage != null)
            OnDamage(shooter);
    }


    [ContextMenu("Test ApplyDamage")]
    public void TestDamage()
    {
        ApplyDamage(null);
    }
}
