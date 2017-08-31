using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputData
{
    public Vector2 Move; 
    public Vector2 Rotation;
    public float Fire; 

    public void Clear()
    {
        Move.Set(0, 0);
        Rotation.Set(0, 0);
        Fire = 0;
    }
}
