using System.Collections.Generic;
using UnityEngine;

public class EnablerOnTrigger : MonoBehaviour
{
    [SerializeField] List<GameObject> activeTargets;
    [SerializeField] bool destroyOnTrigger;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Tags.Player))
        {
            foreach (var g in activeTargets)
            {
                g.SetActive(true);
            }
            if (destroyOnTrigger) Destroy(gameObject);
        }
    }
}
