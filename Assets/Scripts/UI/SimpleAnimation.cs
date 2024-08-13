using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SimpleAnimation : MonoBehaviour
{
    RectTransform rectTransform;
    [SerializeField] Vector3 basePos;
    [SerializeField] Vector3 destination;
    [SerializeField] float moveTime;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        basePos = rectTransform.anchoredPosition;
    }

    public void Play()
    {
        if (rectTransform == null) return;

        Tween tween = rectTransform.DOAnchorPos(destination, moveTime);

        // check if this animation is on a button,
        // if it is disable until finish animation
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.interactable = false;
            tween.OnComplete(() =>
            {
                button.interactable = true;
            });
        }
    }

    public void Rewind()
    {
        if (rectTransform == null) return;

        Tween tween = rectTransform.DOAnchorPos(basePos, moveTime);

        // check if this animation is on a button,
        // if it is disable until finish animation
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.interactable = false;
            tween.OnComplete(() =>
            {
                button.interactable = true;
            });
        }
    }
}
