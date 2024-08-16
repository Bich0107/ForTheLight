using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPlatform : MonoBehaviour
{
    [SerializeField] float existTime;
    [SerializeField] float restoreDelay;
    [SerializeField] bool autoStarted;

    new SpriteRenderer renderer;
    new Collider2D collider;
    bool cycleStarted = false;

    void Awake()
    {
        collider = GetComponent<Collider2D>();
        renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (autoStarted) StartLifeCycle();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == Tags.Player)
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
        collider.enabled = _status;
        renderer.enabled = _status;
    }

    void OnDisable() {
        StopAllCoroutines();
        SetExistanceStatus(true);
        cycleStarted = false;
    }
}
