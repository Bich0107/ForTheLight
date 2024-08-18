using System.Collections;
using UnityEngine;

public class MapAreaLinker : MonoBehaviour
{
    [SerializeField] SaveManager saveManager;
    [SerializeField] MapManager mapManager;
    [SerializeField] int areaIndex;
    [SerializeField] AreaDeactiveAnimation currentAreaAnim;
    [SerializeField] GameObject nextArea;
    [SerializeField] float deactiveDelay = 2f;
    bool triggered = false;

    public int AreaIndex => areaIndex;

    void Start()
    {
        areaIndex = transform.GetSiblingIndex();

        mapManager = FindObjectOfType<MapManager>();
        saveManager = FindObjectOfType<SaveManager>();
    }

    void OnEnable()
    {
        triggered = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        if (other.CompareTag(Tags.Player))
        {
            triggered = true;
            StartCoroutine(CR_UpdateMap());
        }
    }

    IEnumerator CR_UpdateMap()
    {
        // update save file and activate next area;
        saveManager.CurrentSaveFile.AreaIndex = areaIndex + 1;
        mapManager.ActivateArea(areaIndex + 1);
        mapManager.SetUpTriggers();

        // make sure player is not parented by any platform when the area is deactivated
        FindObjectOfType<Player>().transform.parent = null;

        // play shake effect
        currentAreaAnim.Play();

        // turn off flag and deactive area after a delay
        yield return new WaitForSeconds(deactiveDelay);

        // reset area animation
        currentAreaAnim.Reset();

        mapManager.DeactiveArea(areaIndex);
        triggered = false;
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }
}
