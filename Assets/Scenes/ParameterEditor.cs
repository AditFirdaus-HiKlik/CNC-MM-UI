using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class ParameterEditor : MonoBehaviour
{
    public string parameterKey;

    public TMP_Text parameterKeyText;
    public TMP_Text parameterValueText;

    public UnityEvent onNext;
    public UnityEvent onPrevious;
    public UnityEvent onInvoke;

    private void OnValidate()
    {
        SetKeyText(parameterKey);
    }

    public void SetKeyText(string value) => parameterKeyText?.SetText(value);
    public void SetValueText(string value) => parameterValueText?.SetText(value);

    public void Next()
    {
        onNext.Invoke();
        onInvoke.Invoke();
    }

    public void Previous()
    {
        onPrevious.Invoke();
        onInvoke.Invoke();
    }
}
