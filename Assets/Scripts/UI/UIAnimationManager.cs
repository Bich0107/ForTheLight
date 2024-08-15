using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIAnimationManager : MonoBehaviour
{
    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        DOTween.KillAll();
    }
}
