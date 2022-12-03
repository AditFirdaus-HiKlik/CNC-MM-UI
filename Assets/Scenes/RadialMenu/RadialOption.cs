using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(RectTransform))]
public class RadialOption : MonoBehaviour
{
    public bool IsHighlighted = false;
    public UnityEvent OnClick;
    public AudioClip clickSound; // ! Bisa dihilangkan jika tidak ingin menggunakan sound

    public void Click()
    {
        OnClick.Invoke();
        transform.localScale = Vector3.one * 1.75f;
        AudioSource.PlayClipAtPoint(clickSound, Vector3.zero); // ! Bisa dihilangkan jika tidak ingin menggunakan sound
    }

    public void SetHighlight(bool state)
    {
        IsHighlighted = state;
    }

    private void Update()
    {
        // ! Dari Sini
        transform.localScale = Vector3.Lerp(transform.localScale, IsHighlighted ? Vector3.one * 2f : Vector3.one, 0.1f);
        // ! Sampai Sini, bisa dihilangkan jika tidak ingin menambahkan animasi
    }
}
