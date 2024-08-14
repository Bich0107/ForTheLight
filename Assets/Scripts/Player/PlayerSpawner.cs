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
            Debug.LogWarning(name + " can't find object of type SaveManager");
            return;
        }

        // take info from save file
        int areaIndex = saveManager.CurrentSaveFile.AreaIndex;
        
        // activate gun if player has clear guide area
        gun.SetActive(areaIndex > 0);

        player.transform.position = mapManager.GetRespawnPos(areaIndex);
    }

    void Start()
    {
        timeKeeper?.StartTimer();
    }
}
