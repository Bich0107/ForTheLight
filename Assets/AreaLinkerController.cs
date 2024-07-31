using System.Collections.Generic;
using UnityEngine;

public class AreaLinkerController : MonoBehaviour
{
    [SerializeField] List<GameObject> linkers;
    GameObject currentLinker;
    int index = -1;

    void Start()
    {
        EnableNextLinker();
    }

    public void EnableNextLinker() {
        index++;
        if (index >= linkers.Count) return;

        currentLinker?.SetActive(false);
        currentLinker = linkers[index];
        currentLinker?.SetActive(true);
    }
}
