using System.Collections;
using UnityEngine;

public class MapAreaLinker : MonoBehaviour
{
    [SerializeField] SaveManager saveManager;
    [SerializeField] MapManager mapManager;
    [SerializeField] int areaIndex;
    [SerializeField] GameObject nextArea;
    [SerializeField] float deactiveDelay = 2f;

    public int AreaIndex => areaIndex;

    void Start()
    {
        areaIndex = transform.GetSiblingIndex();
        mapManager = FindObjectOfType<MapManager>();
        saveManager = FindObjectOfType<SaveManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Tags.Player))
        {
            nextArea.SetActive(true);
            saveManager.CurrentSaveFile.AreaIndex = areaIndex + 1;
            StartCoroutine(CR_DeactiveArea());
        }
    }

    IEnumerator CR_DeactiveArea()
    {
        yield return new WaitForSeconds(deactiveDelay);
        mapManager.ChangeArea(saveManager.CurrentSaveFile.AreaIndex);
    }
}
