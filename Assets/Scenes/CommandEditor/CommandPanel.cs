using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using TMPro;

public class CommandPanel : MonoBehaviour
{
    CommandView _commandView;
    public bool isFocused;
    public Command command;

    [Header("Content")]
    public UnityEvent<int> OnClick;

    [Header("References")]
    public TMP_Text refNumber;
    public Image refHighlight;
    public Image refCaret;
    public TMP_InputField refCode;

    public int caretPosition;

    [HideInInspector] public int index = 0;
    [HideInInspector] public bool isSelected { set; get; }

    void OnEnable()
    {
        SubscribeCommand();
    }

    void OnDisable()
    {
        UnSubscribeCommand();
    }

    public void SubscribeCommand() => command.onUpdateUI.AddListener(UpdateCodeUI);
    public void UnSubscribeCommand() => command.onUpdateUI.RemoveListener(UpdateCodeUI);

    private void Update()
    {
        ManageHighlight();
        ManageCaret();
        AnimateCaret();
        ManageAnimation();
    }

    public void SetCode(string code, int caret)
    {
        UpdateCode(code);
    }

    public void UpdateCode(string value)
    {
        command.code = value;
        command.UpdateUI();
    }

    public void UpdateCodeUI(string value)
    {
        refCode.text = value;
    }

    public void Init(CommandView commandView, Command command, int index)
    {
        this._commandView = commandView;
        this.command = command;

        this.index = index;

        SetIndexName(this.index);
        UpdateCodeUI(command.code);
        SubscribeCommand();
    }

    public void ManageHighlight()
    {
        if (refCaret.enabled != isSelected) refCaret.enabled = isSelected;
        if (refHighlight.enabled != isSelected) refHighlight.enabled = isSelected;
    }

    public void Select()
    {
        isFocused = true;
        transform.localScale = Vector3.one * 0.9f;
        _commandView?.Select(this);
    }

    public void Deselect()
    {
        isFocused = false;
    }

    public void SetSelectState(bool state) => isSelected = state;

    public void SetIndexName(int index) => refNumber?.SetText($"N{(index + 1).ToString("0000")}");

    void AnimateCaret()
    {
        Vector3 currentPosition = refCaret.rectTransform.anchoredPosition;
        TMP_Text textComponent = refCode.textComponent;

        bool isLengthZero = refCode.text.Length == 0;

        if (!isLengthZero) currentPosition.x = (caretPosition + 1) * textComponent.rectTransform.sizeDelta.x / (textComponent.text.Length - 1);
        else currentPosition.x = 0;

        refCaret.rectTransform.anchoredPosition = Vector3.Lerp(refCaret.rectTransform.anchoredPosition, currentPosition, 0.5f);
    }

    void ManageCaret()
    {
        if (!isFocused)
        {
            if (refCode.caretPosition != caretPosition)
            {
                refCode.caretPosition = caretPosition;
            }
        }
        CommandController._pointerIndex = refCode.caretPosition;
    }

    void ManageAnimation()
    {
        if (transform.localScale != Vector3.one) transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, 0.1f);
    }
}
