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

    void Awake()
    {
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
            SetAreaState(i, i == currentArea);
        }

        triggersManager.SetUpTriggers();
    }

    public void SetAreaState(int index, bool isActive)
    {
        if (index < linkdersList.Count) { linkdersList[index].SetActive(isActive); }
        areaList[index].SetActive(isActive);
        respawnPosList[index].SetActive(isActive);
    }

    public void SetUpTriggers() {
        triggersManager.SetUpTriggers();
    }

    public void ActivateArea(int index) 
    {
        currentArea = index;
        SetAreaState(index, true);
    }

    public void DeactiveArea(int index) => SetAreaState(index, false);

    public void ChangeArea(int previousArea, int nextArea)
    {
        ActivateArea(nextArea);
        DeactiveArea(previousArea);
    }

    public Vector3 GetRespawnPos(int areaIndex)
    {
        return respawnPosList[areaIndex].GetComponent<RespawnPoint>().SpawnPoint;
    }

    public void ResetCurrentMap()
    {
        triggersManager.SetUpTriggers();

        // deactive and active to reset platform animation
        areaList[currentArea].SetActive(false);
        areaList[currentArea].SetActive(true);
    }

    public void Reset()
    {
        currentArea = 0;
        SetUpMap();
    }
}
