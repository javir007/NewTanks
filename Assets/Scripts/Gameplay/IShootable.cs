using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IShootable
{
    Action<IShooter> OnDamage { get; set; }
    void ApplyDamage(IShooter shooter);
}


