using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] List<GameObject> areaList;
    [SerializeField] List<GameObject> areaLinkers;
    [SerializeField] WaveTriggersManager triggersManager;
    int currentArea = 0;

    void OnEnable()
    {
        triggersManager = FindObjectOfType<WaveTriggersManager>();

        SetUpMap();
    }

    public void SetUpMap()
    {
        if (areaLinkers == null || areaList == null) return;

        for (int i = 0; i < areaList.Count; i++)
        {
            if (i == currentArea)
            {
                if (i < areaLinkers.Count) { areaLinkers[i].SetActive(true); }
                areaList[i].SetActive(true);
            }
            else
            {
                if (i < areaLinkers.Count) { areaLinkers[i].SetActive(false); }
                areaList[i].SetActive(false);
            }
        }
        triggersManager.SetUpTriggers();
    }

    public void ChangeArea(int index)
    {
        currentArea = index;
        SetUpMap();
    }

    public void ResetCurrentMap() {
        triggersManager.SetUpTriggers();
        areaList[currentArea].SetActive(false);
        areaList[currentArea].SetActive(true);
    }

    public void Reset()
    {
        ChangeArea(0);
    }
}
