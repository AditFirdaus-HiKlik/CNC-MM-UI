using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using TMPro;

public class ActionPanel : MonoBehaviour
{
    ActionView _actionView;

    [Header("Content")]
    public UnityEvent<int> OnClick;

    [Header("References")]
    public TMP_Text refText;
    public Toggle refToggle;

    [HideInInspector] public int index = 0;

    public void Init(ActionView actionView, int index)
    {
        this._actionView = actionView;
        this.refToggle.group = actionView.toggleGroup;

        this.index = index;

        SetIndexName(this.index);
    }

    private void OnEnable()
    {
        refToggle.onValueChanged.AddListener(Click);
    }

    private void OnDisable()
    {
        refToggle.onValueChanged.RemoveListener(Click);
    }

    public void Click(bool state)
    {
        if (state)
        {
            _actionView?.Select(this);
        }
    }

    public void SetIndexName(int index)
    {
        refText.text = $"O{(index + 1).ToString("0000")}";
    }
}
