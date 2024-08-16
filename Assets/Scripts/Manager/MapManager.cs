using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] SaveManager saveManager;
    [SerializeField] List<GameObject> areaList;
    [SerializeField] List<GameObject> linkersList;
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
        currentArea = saveManager.CurrentSaveFile.AreaIndex;

        if (linkersList == null || areaList == null) return;

        for (int i = 0; i < areaList.Count; i++)
        {
            SetAreaState(i, i == currentArea);
        }

        SetUpTriggers();
    }

    public void SetAreaState(int index, bool isActive)
    {
        if (index < linkersList.Count) { linkersList[index].SetActive(isActive); }
        areaList[index].SetActive(isActive);
        respawnPosList[index].SetActive(isActive);
    }

    public void SetUpTriggers() {
        triggersManager.SetUpTriggers();
    }

    public void ActivateArea(int index) => SetAreaState(index, true);

    public void DeactiveArea(int index) => SetAreaState(index, false);

    public void ChangeArea(int nextArea)
    {
        DeactiveArea(saveManager.CurrentSaveFile.AreaIndex);
        ActivateArea(nextArea);
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
