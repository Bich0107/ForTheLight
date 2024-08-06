using System.Collections.Generic;
using UnityEngine;

public class AreaLinkerController : MonoBehaviour
{
    [SerializeField] List<GameObject> linkers;
    GameObject currentLinker;
    [SerializeField] int enemyCount = 1; // 1 for the first enemy at guide area
    public int EnemyCount
    {
        get { return enemyCount; }
        set
        {
            enemyCount = value;
            if (enemyCount <= 0) ready = true;
        }
    }

    bool ready = false;
    int index = -1;

    void Start()
    {
        DeactiveAllLinkers();
    }

    void Update()
    {
        if (enemyCount == 0 && ready)
        {
            EnableNextLinker();
        }
    }

    public void EnableNextLinker()
    {
        index++;
        if (index >= linkers.Count) return;

        currentLinker?.SetActive(false);
        currentLinker = linkers[index];
        currentLinker?.SetActive(true);

        ready = false;
    }

    void DeactiveAllLinkers()
    {
        foreach (GameObject g in linkers)
        {
            g.SetActive(false);
        }
    }
}
