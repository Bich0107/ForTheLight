using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIAnimationManager : MonoSingleton<UIAnimationManager>
{
    new void Awake()
    {
        base.Awake();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        DOTween.KillAll();
    }
}
