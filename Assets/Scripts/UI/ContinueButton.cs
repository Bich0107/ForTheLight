using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{
    [SerializeField] Color activeColor;
    [SerializeField] Color disableColor;
    Image image;
    Button button;
    SaveManager saveManager;

    void Start()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        saveManager = FindObjectOfType<SaveManager>();

        if (saveManager.CurrentSaveFile == null)
        {
            image.color = disableColor;
            button.interactable = false;
        }
        else
        {
            image.color = activeColor;
            button.interactable = true;
        }
    }
}
