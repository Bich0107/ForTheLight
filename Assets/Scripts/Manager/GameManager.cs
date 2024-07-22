using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    public bool gameStarted;

    new void Awake()
    {
        base.Awake();

        DOTween.Init();
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene(0);
        } else if (Input.GetKeyDown(KeyCode.Z))
        {
            gameStarted = true;
        }
#endif
    }
}
