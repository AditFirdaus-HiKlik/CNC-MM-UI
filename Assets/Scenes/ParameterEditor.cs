using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class ParameterEditor : MonoBehaviour
{
    public bool editable = true;
    public string parameterKey;

    public TMP_Text parameterKeyText;
    public TMP_InputField parameterValueInputField;

    public UnityEvent onNext;
    public UnityEvent onPrevious;
    public UnityEvent onInvoke;
    public UnityEvent<string> onSubmit;

    private void OnValidate()
    {
        Validate();
    }

    private void Start()
    {
        Validate();
    }

    public void Validate()
    {
        SetKeyText(parameterKey);
        parameterValueInputField.interactable = editable;
    }

    public void SetKeyText(string value) => parameterKeyText?.SetText(value);
    public void SetValueText(string value)
    {
        if (parameterValueInputField) parameterValueInputField.text = value;
    }

    public void Submit(string value)
    {
        onSubmit.Invoke(value);
    }

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
