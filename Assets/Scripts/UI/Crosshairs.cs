using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshairs : MonoBehaviour
{
    [SerializeField] RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        HideCursor();
        SetCursorPosition();
    }

    void HideCursor() => Cursor.visible = false;

    void SetCursorPosition()
    {
        Vector2 mousePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform.parent as RectTransform,
            Input.mousePosition,
            null,
            out mousePos);
        rectTransform.localPosition = mousePos;
    }

    void OnDisable()
    {
        Cursor.visible = true;
    }
}
