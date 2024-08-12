using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{
    Button button;
    SaveManager saveManager;

    void Start()
    {
        button = GetComponent<Button>();
        saveManager = FindObjectOfType<SaveManager>();

        if (saveManager.CurrentSaveFile == null)
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }
    }
}
