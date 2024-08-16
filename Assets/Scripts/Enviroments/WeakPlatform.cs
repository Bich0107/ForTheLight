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
    bool circleStarted = false;

    void Awake()
    {
        collider = GetComponent<Collider2D>();
        renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (autoStarted) StartLifeCircle();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == Tags.Player)
        {
            StartLifeCircle();
        }
    }

    void StartLifeCircle()
    {
        if (circleStarted) return;
        circleStarted = true;
        StartCoroutine(CR_LifeCircle());
    }

    IEnumerator CR_LifeCircle()
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
        circleStarted = false;
    }
}
