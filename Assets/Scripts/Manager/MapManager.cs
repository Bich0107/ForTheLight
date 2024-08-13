using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] SaveManager saveManager;
    [SerializeField] List<GameObject> areaList;
    [SerializeField] List<GameObject> linkdersList;
    [SerializeField] List<GameObject> respawnPosList;
    [SerializeField] WaveTriggersManager triggersManager;
    int currentArea = 0;

    void Awake() {
        saveManager = FindObjectOfType<SaveManager>();

        currentArea = saveManager.CurrentSaveFile.AreaIndex;    
    }

    void OnEnable()
    {
        triggersManager = FindObjectOfType<WaveTriggersManager>();

        SetUpMap();
    }

    public void SetUpMap()
    {
        if (linkdersList == null || areaList == null) return;

        for (int i = 0; i < areaList.Count; i++)
        {
            if (i == currentArea)
            {
                if (i < linkdersList.Count) { linkdersList[i].SetActive(true); }
                areaList[i].SetActive(true);
                respawnPosList[i].SetActive(true);
            }
            else
            {
                if (i < linkdersList.Count) { linkdersList[i].SetActive(false); }
                areaList[i].SetActive(false);
                respawnPosList[i].SetActive(false);
            }
        }
        triggersManager.SetUpTriggers();
    }

    public void ChangeArea(int index)
    {
        currentArea = index;
        SetUpMap();
    }

    public Vector3 GetRespawnPos(int areaIndex) {
        return respawnPosList[areaIndex].GetComponent<RespawnPoint>().SpawnPoint;
    }

    public void ResetCurrentMap() {
        triggersManager.SetUpTriggers();

        // deactive and active to reset platform animation
        areaList[currentArea].SetActive(false);
        areaList[currentArea].SetActive(true);
    }

    public void Reset()
    {
        ChangeArea(0);
    }
}
