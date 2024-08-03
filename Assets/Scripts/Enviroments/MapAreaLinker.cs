using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapAreaLinker : MonoBehaviour
{
    [SerializeField] GameObject previousArea;
    [SerializeField] GameObject nextArea;
    [SerializeField] float deactiveDelay = 2f;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag(Tags.Player)) {
            StartCoroutine(CR_DeactiveArea());
            nextArea.SetActive(true);
        }
    }

    IEnumerator CR_DeactiveArea() {
        yield return new WaitForSeconds(deactiveDelay);
        previousArea.SetActive(false);
        Destroy(gameObject);
    }
}
