using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] TimeKeeper timeKeeper;
    [SerializeField] SaveManager saveManager;
    [SerializeField] MapManager mapManager;
    [SerializeField] EnemySpawner enemySpawner;

    public bool gameStarted;
    bool playerControl = true;
    public bool PlayerControlStatus() => playerControl;

    new void Awake()
    {
        base.Awake();

        DOTween.Init();
        DOTween.defaultTimeScaleIndependent = true;
        
        timeKeeper = FindObjectOfType<TimeKeeper>();
        saveManager = FindObjectOfType<SaveManager>();
        mapManager = FindObjectOfType<MapManager>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }
    
#if UNITY_EDITOR
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
#endif

    public void Pause() {
        SetPlayerControlStatus(false);
        Time.timeScale = 0f;
    }

     public void Resume() {
        SetPlayerControlStatus(true);
        Time.timeScale = 1f;
    }

    public void PlayerRespawn()
    {
        ObjectPool.Instance.Reset();
        enemySpawner.Reset();
        mapManager.ResetCurrentMap();
    }

    public void TogglePlayerControl()
    {
        playerControl = !playerControl;
    }

    public void SetPlayerControlStatus(bool _value)
    {
        playerControl = _value;
    }

    public void GameOver()
    {
        timeKeeper.StopTimer();

        FindObjectOfType<SceneLoader>().LoadGameOverScene();
    }
}
