using UnityEngine;
using UnityEngine.UI;

public class SFXVolumeSlider : MonoBehaviour
{
    [SerializeField] Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();

        slider.value = AudioManager.Instance.CurrentSfxVolume;

        slider.onValueChanged.AddListener((value) => {
            AudioManager.Instance.ChangeSFXVolume(value);
        });
    }
}
