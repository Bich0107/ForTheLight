using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickSound : MonoBehaviour
{
    [SerializeField] AudioClip buttonClickSound;
    [SerializeField] List<Button> buttons;

    void Start()
    {
        AddSoundOnClickEvent();
    }

    void AddSoundOnClickEvent()
    {
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => PlayClickSound());
        }
    }

    void PlayClickSound()
    {
        if (buttonClickSound == null) return;
        
        AudioManager.Instance.PlaySound(buttonClickSound);
    }
}
