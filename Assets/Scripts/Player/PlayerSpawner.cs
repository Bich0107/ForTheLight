using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] TimeKeeper timeKeeper;
    [SerializeField] SaveManager saveManager;
    [SerializeField] MapManager mapManager;
    [SerializeField] GameObject player;
    [SerializeField] Vector3 offset;

    void Awake()
    {
        saveManager = FindObjectOfType<SaveManager>();
        mapManager = FindObjectOfType<MapManager>();
        timeKeeper = FindObjectOfType<TimeKeeper>();

        if (saveManager == null)
        {
            Debug.LogWarning(name + " can't find object of type SaveManager");
            return;
        }

        int areaIndex = saveManager.CurrentSaveFile.AreaIndex;
        player.transform.position = mapManager.GetRespawnPos(areaIndex);
    }

    void Start()
    {
        timeKeeper?.StartTimer();
    }
}
