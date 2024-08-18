using UnityEngine;

public class BGMSetter : MonoBehaviour
{
    [SerializeField] BGM bgm;

    void Start()
    {
        SetBGM();
    }

    void SetBGM() {
        AudioManager.Instance.SetBGM(bgm);
    }
}
