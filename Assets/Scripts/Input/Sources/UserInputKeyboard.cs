using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInputKeyboard : MonoBehaviour, IUserInput
{
    public UserInputSource Source {  get { return UserInputSource.Keyboard; } }
   
    public void ProcessInput(InputData inputData)
    {
        inputData.Move.x = Input.GetAxis("Horizontal") * Time.deltaTime * 50f;
        inputData.Move.y = Input.GetAxis("Vertical") * Time.deltaTime * 50f;
        inputData.Fire += Input.GetKeyUp(KeyCode.Space) ? 1 : 0;
    }
}
