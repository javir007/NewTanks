using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoSingleton<UserInput>
{
    public InputData InputRequested = new InputData();
    List<IUserInput> inputSources = new List<IUserInput>();
    List<Action<InputData>> customInputs = new List<Action<InputData>>();

    protected override void Initialize()
    {
        var allSources = new List<IUserInput>(GetComponents<IUserInput>());

        if (Application.isEditor || !Application.isMobilePlatform)
        {
            var keyboard = allSources.Find(input => input.Source == UserInputSource.Keyboard);
            var mouse = allSources.Find(input => input.Source == UserInputSource.Mouse);
            if (keyboard != null) inputSources.Add(keyboard);
            if (mouse != null) inputSources.Add(mouse);
        }

        if (Application.isMobilePlatform)
        {
            var mobile = allSources.Find(el => el.Source == UserInputSource.Mobile);
            if (mobile != null) inputSources.Add(mobile);
        }
    }

    public void AddCustomInput(Action<InputData> customInput)
    {
        customInputs.Add(customInput);
    }

    void Update()
    {
        InputRequested.Clear();

        if (customInputs.Count > 0)
        {
            foreach (Action<InputData> custom in customInputs)
                custom(InputRequested);
            customInputs.Clear();
        }

        foreach (IUserInput source in inputSources)
            source.ProcessInput(InputRequested);
    }
}
