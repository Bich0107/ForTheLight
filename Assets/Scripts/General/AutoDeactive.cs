using System.Collections;
using UnityEngine;

public class AutoDeactive : MonoBehaviour
{
    [SerializeField] float delay;
    Coroutine coroutine;

    void OnEnable() {
        coroutine = StartCoroutine(CR_Deactive());
    }

    IEnumerator CR_Deactive() {
        yield return new WaitForSeconds(delay);

        gameObject.SetActive(false);
    }

    void OnDisable() {
        if (coroutine != null) {
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }
}
