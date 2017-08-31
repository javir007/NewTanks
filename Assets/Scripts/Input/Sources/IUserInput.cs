using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUserInput
{
    UserInputSource Source { get; }
    void ProcessInput(InputData inputData);
}

public enum UserInputSource
{
    Keyboard,
    Mouse,
    Mobile,
    Joystick
}