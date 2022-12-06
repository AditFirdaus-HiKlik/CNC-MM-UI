using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Command
{
    public string hash = Guid.NewGuid().ToString();
    public string code = "";

    [System.NonSerialized] public UnityEvent<string> onUpdateUI = new UnityEvent<string>();

    public void UpdateUI()
    {
        onUpdateUI.Invoke(code);
    }
}
