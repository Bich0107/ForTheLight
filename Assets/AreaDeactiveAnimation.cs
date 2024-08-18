using System;
using UnityEngine;
using DG.Tweening;

public class AreaDeactiveAnimation : MonoBehaviour
{
    [SerializeField] float duration;
    [SerializeField] float power;
    [SerializeField] float dropHeight;
    [SerializeField] Vector3 originalPosition;
    [SerializeField] AudioClip shakeSfx;
    Tween shakeTween;
    Tween dropdownTween;

    public void Play()
    {
        // Save the original position
        originalPosition = transform.localPosition;

        // Create a shake tween and play sfx
        Shake();

        // slowy drop down
        DropDown();
    }

    void Shake()
    {
        shakeTween = transform.DOShakePosition(duration, power);
        AudioManager.Instance.PlaySound(shakeSfx);
    }

    void DropDown()
    {
        dropdownTween = transform.DOMoveY(dropHeight, duration).SetEase(Ease.InExpo);
    }

    public void Reset() {
         // return to original position
        transform.localPosition = originalPosition;

        // kill all tweens
        shakeTween.Kill();
        dropdownTween.Kill();
    }
}
