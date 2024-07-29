using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    public bool gameStarted;
    bool playerControl = true;

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

    public void SetPlayerControlStatus(bool _value) {
        playerControl = _value;
    }

    public bool PlayerControlStatus() => playerControl;
}
