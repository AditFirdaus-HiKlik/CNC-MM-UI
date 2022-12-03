using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class KeyButton : MonoBehaviour
{
    KeyPad _keyPad;
    public KeyPad keyPad
    {
        get
        {
            if (_keyPad == null) _keyPad = GetComponentInParent<KeyPad>();
            return _keyPad;
        }
    }

    public bool textAsValue = true;
    public bool specialKey = false;

    [Header("Content")]
    public string keyText;
    public string keyValue;
    public Sprite keyIcon;
    public Color keyColor;
    public UnityEvent OnClick;
    public AudioClip buttonSound; // ! Bisa dihilangkan jika tidak ingin menggunakan sound

    [Header("References")]
    public TMP_Text refText;
    public Image refIcon;
    public Image refBackground;
    public Button refButton;

    private void OnValidate()
    {
        SetText(keyText);
        SetIcon(keyIcon);
        SetColor(keyColor);

        UpdateName();
    }

    private void OnEnable()
    {
        refButton.onClick.AddListener(Click);
        if (textAsValue) keyValue = keyText;
    }

    private void OnDisable()
    {
        refButton.onClick.RemoveListener(Click);
    }

    public void Click()
    {
        OnClick.Invoke();

        if (specialKey)
            keyPad.KeyPadInputSpecial(keyValue);
        else
            keyPad.KeyPadInput(keyValue);

        AudioSource.PlayClipAtPoint(buttonSound, Vector3.zero); // ! Bisa dihilangkan jika tidak ingin menggunakan sound
    }

    public void UpdateName()
    {
        if (keyText != "")
            gameObject.name = $"{keyText}";
        else if (keyValue != "")
            gameObject.name = $"{keyValue}";
        else
            gameObject.name = "KeyButton";
    }

    public void SetText(string text)
    {
        if (!refText) return;
        refText.text = keyText = text;
    }

    public void SetIcon(Sprite icon)
    {
        if (!refIcon) return;
        refIcon.sprite = keyIcon = icon;

        if (keyIcon != null)
        {
            refIcon.enabled = true;
        }
        else
        {
            refIcon.enabled = false;
        }
    }

    public void SetColor(Color color)
    {
        if (!refBackground) return;
        refBackground.color = keyColor = color;
    }
}
