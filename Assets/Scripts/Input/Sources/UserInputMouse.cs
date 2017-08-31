using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInputMouse : MonoBehaviour, IUserInput
{
    public UserInputSource Source {  get { return UserInputSource.Mouse; } }
   
    public void ProcessInput(InputData inputData)
    {
        
        inputData.Fire += Input.GetMouseButtonUp(0) ? 1 : 0;
    }
}
