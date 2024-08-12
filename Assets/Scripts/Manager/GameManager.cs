using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] SaveManager saveManager;
    [SerializeField] MapManager mapManager;
    [SerializeField] EnemySpawner enemySpawner;

    public bool gameStarted;
    bool playerControl = true;

    new void Awake()
    {
        base.Awake();

        saveManager = FindObjectOfType<SaveManager>();
        mapManager = FindObjectOfType<MapManager>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
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

    public void PlayerRespawn() {
        ObjectPool.Instance.Reset();
        enemySpawner.Reset();
        mapManager.ResetCurrentMap();
    }

    public void TogglePlayerControl() {
        playerControl = !playerControl;
    }

    public void SetPlayerControlStatus(bool _value) {
        playerControl = _value;
    }

    public bool PlayerControlStatus() => playerControl;
}
