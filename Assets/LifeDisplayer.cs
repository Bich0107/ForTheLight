using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LifeDisplayer : MonoBehaviour
{
    [SerializeField] List<LifeIcon> lives;
    [SerializeField] SaveManager saveManager;
    int maxLive;
    int currentLive;

    void Awake()
    {
        saveManager = FindObjectOfType<SaveManager>();

        SetUpLives();
    }

    void SetUpLives()
    {
        SetMaxLife();
        SetCurrentLife();
    }

    void SetMaxLife()
    {
        maxLive = saveManager.CurrentSaveFile.MaxLife;

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);

            child.gameObject.SetActive(i < maxLive);
        }
    }

    void SetCurrentLife()
    {
        currentLive = saveManager.CurrentSaveFile.CurrentLife;

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);

            LifeIcon icon = child.GetComponent<LifeIcon>();
            if (i < currentLive)
                icon.TurnOn();
            else
                icon.TurnOff();
        }
    }

    public void UpdateLife()
    {
        SetCurrentLife();
    }
}
