using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AK47Gun : Gun
{
    [Header("AK47 settings")]
    [SerializeField] float minFireCD = 0.2f;
    [SerializeField] float baseFireCD = 0.75f;
    [SerializeField] float cdReduceTime = 2f;
    [SerializeField] bool isShooting = false;
    float currentFireCD;
    float timer;
    Quaternion rotation;
    Coroutine reduceCDCoroutine;

    void OnEnable()
    {
        currentFireCD = baseFireCD;
        timer = currentFireCD;
    }

    void Update()
    {
        if (!isShooting) return;

        if (timer < currentFireCD)
        {
            timer += Time.deltaTime;
        }

        CheckTimer();
    }

    void CheckTimer()
    {
        if (timer >= currentFireCD)
        {
            timer = 0f;
            Shoot(rotation);
        }
    }

    IEnumerator CR_ReduceCD()
    {
        float tick = 0f;
        float startValue = currentFireCD;
        while (!Mathf.Approximately(currentFireCD, minFireCD))
        {
            tick += Time.deltaTime;
            currentFireCD = Mathf.Lerp(startValue, minFireCD, tick / cdReduceTime);
            yield return null;
        }

        currentFireCD = minFireCD;
    }

    public override void HoldTrigger(Quaternion _rotation)
    {
        isShooting = true;
        rotation = _rotation;

        if (reduceCDCoroutine == null)
        {
            currentFireCD = baseFireCD;
            reduceCDCoroutine = StartCoroutine(CR_ReduceCD());
        }
    }

    public override void ReleaseTrigger(Quaternion _rotation)
    {
        Reset();
    }

    public override void Reset()
    {
        timer = baseFireCD;

        isShooting = false;
        
        if (reduceCDCoroutine != null)
        {
            StopCoroutine(reduceCDCoroutine);
            reduceCDCoroutine = null;
        }
    }
}
