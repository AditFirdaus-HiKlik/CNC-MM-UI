using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using TMPro;

public class CommandPanel : MonoBehaviour
{
    CommandView _commandView;
    public Command command;

    [Header("Content")]
    public float characterWidth = 1;
    public float caretBlink = 0.1f;
    public UnityEvent<int> OnClick;

    [Header("References")]
    public TMP_Text refText;
    public TMP_Text refCode;
    public Image caret;
    public Toggle refToggle;

    [HideInInspector] public int index = 0;

    float caretTime;

    private void Update()
    {
        UpdateCode();

        if (refToggle.isOn && caretTime >= caretBlink)
        {
            caret.enabled = !caret.enabled;
            caretTime = 0;
        }
        else caretTime += Time.deltaTime;
    }

    public void Init(CommandView commandView, Command command, int index)
    {
        this._commandView = commandView;
        this.command = command;
        this.refToggle.group = commandView.toggleGroup;

        this.index = index;

        SetIndexName(this.index);
        SetCaretIndex(0);
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
            _commandView?.Select(this);
            caret.enabled = true;
        }
        else
        {
            caret.enabled = false;
        }
    }

    public void SetIndexName(int index)
    {
        refText.text = $"N{(index + 1).ToString("0000")}";
    }

    public void UpdateCode()
    {
        if (refCode.text != command.code)
        {
            refCode.text = command.code;
        }
    }

    public void SetCaretIndex(int caretIndex)
    {
        Debug.Log(caretIndex);
        caret.transform.localPosition = new Vector3((caretIndex + 1) * characterWidth, 0, 0);
    }
}
