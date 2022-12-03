using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

public class ParameterButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Image buttonImage;
    public bool loop = false;
    public float invokeLoopDelay = 0.1f;
    public float invokeLoopInterval = 0.1f;
    public UnityEvent onInvoke;

    [Header("Parameters")]
    public Color idleButtonColor;
    public Color invokeButtonColor;
    public AudioClip invokeSound;

    bool onHold;

    private void Start()
    {
        StartCoroutine(ButtonInvokeCoroutine());
        StartCoroutine(ButtonColorCoroutine());
    }

    private void OnValidate()
    {
        if (buttonImage) buttonImage.color = idleButtonColor;
    }

    public void OnPointerDown(PointerEventData eventData) => onHold = true;

    public void OnPointerUp(PointerEventData eventData) => onHold = false;

    public void InvokeButton()
    {
        onInvoke.Invoke();
        buttonImage.color = invokeButtonColor;
        AudioSource.PlayClipAtPoint(invokeSound, Vector3.zero);
    }

    IEnumerator ButtonInvokeCoroutine()
    {
        while (true)
        {
            if (onHold)
            {
                InvokeButton();

                float loopDelayTime = 0;

                while (onHold && loopDelayTime < invokeLoopDelay) // Jika masih menahan Button dan dia masih belum mencapai limit delay
                {
                    yield return new WaitForEndOfFrame();
                    loopDelayTime += Time.deltaTime;
                }

                while (onHold)
                {
                    InvokeButton();
                    yield return new WaitForSeconds(invokeLoopInterval);
                }
            }

            yield return null;
        }
    }

    IEnumerator ButtonColorCoroutine()
    {
        while (true)
        {
            if (buttonImage.color != idleButtonColor)
            {
                buttonImage.color = Color.Lerp(buttonImage.color, idleButtonColor, 0.1f);
            }

            yield return null;
        }
    }
}
