using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapAreaLinker : MonoBehaviour
{
    [SerializeField] GameObject previousArea;
    [SerializeField] GameObject nextArea;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag(Tags.Player)) {
            nextArea.SetActive(true);
            previousArea.SetActive(false);
            Destroy(gameObject);
        }
    }
}
