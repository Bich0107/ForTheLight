using UnityEngine;
using UnityEngine.UI;

public class BGMVolumeSlider : MonoBehaviour
{
    [SerializeField] Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();

        slider.value = AudioManager.Instance.CurrentBGMVolume;
        
        slider.onValueChanged.AddListener((value) => {
            AudioManager.Instance.ChangeBGMVolume(value);
        });
    }
}
