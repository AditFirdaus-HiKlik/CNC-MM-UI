using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyPad : MonoBehaviour
{
    public UnityEvent<string> OnInputKey;
    public UnityEvent<string> OnInputSpecialKey;

    public void KeyPadInput(string code)
    {
        OnInputKey.Invoke(code);
    }

    public void KeyPadInputSpecial(string code)
    {
        OnInputSpecialKey.Invoke(code);
    }
}
