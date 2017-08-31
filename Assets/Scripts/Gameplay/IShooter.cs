using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShooter
{
    int DamageRate { get; }
    void Fire();
}
