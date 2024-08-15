using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] TimeKeeper timeKeeper;
    [SerializeField] SaveManager saveManager;
    [SerializeField] MapManager mapManager;
    [SerializeField] GameObject player;
    [SerializeField] GameObject gun;

    void Awake()
    {
        saveManager = FindObjectOfType<SaveManager>();
        mapManager = FindObjectOfType<MapManager>();
        timeKeeper = FindObjectOfType<TimeKeeper>();

        if (saveManager == null)
        {
            Debug.LogWarning(name + " can't find object of type " + saveManager.GetType().ToString());
            return;
        }

        Spawn();
    }

    void Start()
    {
        timeKeeper?.StartTimer();
    }

    public void Spawn(int _index = -1)
    {
        int areaIndex;

        // get area index, default is from save file
        if (_index == -1)
        {
            areaIndex = saveManager.CurrentSaveFile.AreaIndex;
        }
        else
        {
            areaIndex = _index;
        }

        // activate gun if player has clear guide area
        gun.SetActive(areaIndex > 0);

        player.transform.position = mapManager.GetRespawnPos(areaIndex);
    }
}
