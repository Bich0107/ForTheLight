using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveTriggersManager : MonoBehaviour
{
    [SerializeField] SaveManager saveManager;
    [SerializeField] List<EnemyWaveTrigger> triggersList;

    void Awake()
    {
        saveManager = FindObjectOfType<SaveManager>();

        foreach (Transform child in transform)
        {
            child.GetComponent<EnemyWaveTrigger>();
        }

        SetUpTriggers();
    }

    public void SetUpTriggers()
    {
        int index = saveManager.CurrentSaveFile.AreaIndex;
        foreach (var trigger in triggersList)
        {
            if (trigger.AreaIndex == index)
            {
                trigger.gameObject.SetActive(true);
            }
            else
            {
                trigger.gameObject.SetActive(false);
            }
        }
    }
}
