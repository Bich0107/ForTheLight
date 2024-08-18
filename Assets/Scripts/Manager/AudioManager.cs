using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BGM
{
    StartScene,
    PlayScene,
    GameOverScene
}

public class AudioManager : MonoSingleton<AudioManager>
{
    [Header("BGM settings")]
    [SerializeField] BGM currentBGM;
    [SerializeField] List<AudioClip> bgmList;
    [SerializeField] AudioSource bgmSource;
    [SerializeField] float baseBGMVolume;
    [SerializeField] float currentBGMVolume;
    public float CurrentBGMVolume => currentBGMVolume;
    [SerializeField] float bgmDecreaseTime;
    [SerializeField] float bgmIncreaseTime;
    [Tooltip("When the current bgm play past this duration percent, loop it back")]
    [Range(0f, 100f)]
    [SerializeField] float loopingPercent;
    bool isChangingVolume = false;
    bool isResetingBGM = false;

    [Header("Sound effect settings")]
    [SerializeField] AudioSource sfxSource;
    [SerializeField] float baseSfxVolume;
    [SerializeField] float currentSfxVolume;
    public float CurrentSfxVolume => currentSfxVolume;

    new void Awake()
    {
        base.Awake();
        SetupVolume();
    }

    void Update()
    {
        LoopBGM();
    }

    void SetupVolume()
    {
        bgmSource.volume = baseBGMVolume;
        currentBGMVolume = baseBGMVolume;

        sfxSource.volume = baseSfxVolume;
        currentSfxVolume = baseSfxVolume;
    }

    #region BGM methods
    public void ChangeBGMVolume(float _value)
    {
        bgmSource.volume = _value;
        currentBGMVolume = _value;
    }

    public void LoopBGM()
    {
        float playPercent = bgmSource.time / bgmSource.clip.length * 100f;

        if (playPercent > loopingPercent && !isResetingBGM)
        {
            isResetingBGM = true;
            SetBGM(currentBGM);
        }
    }

    public void SetBGM(BGM bgm)
    {
        int index = (int)bgm;

        if (index >= bgmList.Count)
        {
            Debug.Log("bgmList: index is out of range");
            return;
        }

        AudioClip clip = bgmList[index];
        StartCoroutine(CR_ChangeBGM(clip));
    }

    IEnumerator CR_ChangeBGM(AudioClip clip)
    {
        if (bgmSource == null)
        {
            Debug.Log("bgmSource is null");
            yield break;
        }

        // if there is a bgm currently playing
        if (bgmSource.clip != null)
        {
            // slowy change current BGM volume to 0
            StartCoroutine(CR_ChangeBGMVolume(0f, bgmDecreaseTime));

            // wait until the volume reach 0
            while (isChangingVolume) yield return null;
        }

        // reset audio source and set the new BGM
        bgmSource.Stop();
        bgmSource.clip = clip;
        bgmSource.Play();

        // slowly increase the volume back up
        StartCoroutine(CR_ChangeBGMVolume(currentBGMVolume, bgmIncreaseTime));

        // wait until finish changing volume
        while (isChangingVolume) yield return null;

        // turn off reseting flag 
        isResetingBGM = false;
    }

    IEnumerator CR_ChangeBGMVolume(float targetValue, float changeTime)
    {
        // raise flag to know the change is still happening
        isChangingVolume = true;

        float tick = 0f;

        // slowly change the volume value
        float startValue = bgmSource.volume;
        while (!Mathf.Approximately(bgmSource.volume, targetValue))
        {
            tick += Time.unscaledDeltaTime;
            bgmSource.volume = Mathf.Lerp(startValue, targetValue, tick / changeTime);
            yield return null;
        }

        // ensure the volume is equal to the target value
        bgmSource.volume = targetValue;

        isChangingVolume = false;
    }
    #endregion

    #region Sfx methods
    public void ChangeSFXVolume(float _value)
    {
        sfxSource.volume = _value;
        currentSfxVolume = _value;
    }

    public void PlaySound(AudioClip clip)
    {
        if (sfxSource == null)
        {
            Debug.Log("sfxSource is null");
            return;
        }

        if (clip == null)
        {
            Debug.Log("sound clip is null");
            return;
        }

        sfxSource.PlayOneShot(clip);
    }
    #endregion
}
