using UnityEngine;
using UnityEngine.InputSystem;

public class Tester : MonoBehaviour
{
    MapManager map;
    SaveManager saveManager;

    ObjectPool objectPool;
    Player player;
    PlayerSpawner spawner;
    EnemySpawner enemySpawner;

    LifeDisplayer life;
    HealthController healthController;

    void Start()
    {
        healthController = GetComponent<HealthController>();

        player = FindObjectOfType<Player>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
        objectPool = FindObjectOfType<ObjectPool>();
        saveManager = FindObjectOfType<SaveManager>();
        spawner = FindObjectOfType<PlayerSpawner>();
        map = FindObjectOfType<MapManager>();
        life = FindObjectOfType<LifeDisplayer>();
    }

    void OnCheat(InputValue inputValue)
    {
        int cheatType = (int)inputValue.Get<float>();

        switch (cheatType)
        {
            case 1:
                healthController.ToggleCheat();
                break;
            case 2:
                life.ToggleCheat();
                break;
        }
    }

    void OnChangeMap(InputValue inputValue)
    {
        int areaIndex = (int)inputValue.Get<float>();

        saveManager.CurrentSaveFile.AreaIndex = areaIndex;

        player.transform.parent = null;
        enemySpawner.Reset();
        objectPool.Reset();
        map.ActivateArea(areaIndex);
        map.SetUpMap();
        spawner.Spawn(areaIndex);
    }
}