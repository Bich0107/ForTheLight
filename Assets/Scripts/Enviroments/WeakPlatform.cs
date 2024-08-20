using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPlatform : MonoBehaviour
{
    [SerializeField] float existTime;
    [SerializeField] float restoreDelay;

    SpriteRenderer spriteRenderer;
    Collider2D bodyCollider;
    bool cycleStarted = false;

    void Awake()
    {
        bodyCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(Tags.Player))
        {
            StartLifeCycle();
        }
    }

    void StartLifeCycle()
    {
        if (cycleStarted) return;

        cycleStarted = true;
        StartCoroutine(CR_LifeCycle());
    }

    IEnumerator CR_LifeCycle()
    {
        do
        {
            yield return new WaitForSeconds(existTime);
            SetExistanceStatus(false);
            yield return new WaitForSeconds(restoreDelay);
            SetExistanceStatus(true);
        } while (true);
    }

    void SetExistanceStatus(bool _status)
    {
        bodyCollider.enabled = _status;
        spriteRenderer.enabled = _status;
    }

    void OnDisable() {
        StopAllCoroutines();
        SetExistanceStatus(true);
        cycleStarted = false;
    }
}
