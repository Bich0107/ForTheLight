using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LifeDisplayer : MonoBehaviour
{
    [SerializeField] GameObject lifeIcon;
    [SerializeField] List<LifeIcon> lives;
    [SerializeField] SaveManager saveManager;
    int maxLive;
    int currentLive;

    void Start()
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

        for (int i = 0; i < maxLive; i++)
        {
            GameObject live = Instantiate(lifeIcon);
            live.transform.SetParent(transform, false);
            lives.Add(live.GetComponent<LifeIcon>());
        }
    }

    void SetCurrentLife()
    {
        currentLive = saveManager.CurrentSaveFile.CurrentLife;

        for (int i = 0; i < lives.Count; i++)
        {
            if (i < currentLive)
            {
                lives[i].TurnOn();
            }
            else
            {
                lives[i].TurnOff();
            }
        }
    }

    public void DecreaseLife(int _value)
    {
        saveManager.CurrentSaveFile.CurrentLife -= _value;
        UpdateLife();
    }

    public void UpdateLife()
    {
        SetCurrentLife();
    }
}
