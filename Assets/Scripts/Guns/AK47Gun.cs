using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AK47Gun : Gun
{
    [Header("AK47 settings")]
    [SerializeField] float minFireCD = 0.2f;
    [SerializeField] float baseFireCD = 0.75f;
    [Tooltip("Time for the cd to change from base value to min value")]
    [SerializeField] float cdReduceTime = 2f;
    [Space]
    [SerializeField] Slider overheatBar;
    [SerializeField] float overheatValue;
    [SerializeField] float currentHeatValue;
    [SerializeField] float heatPerShot;
    [SerializeField] float cooldownTime;
    [SerializeField] float minCooldownPower = 0.4f;
    [SerializeField] bool overheated = false;
    float cooldownPower; // use to make the effect when the more player shoot, the faster it cooldown
    float deactiveTime;
    [Space]
    [SerializeField] bool isShooting = false;
    float currentFireCD;
    float timer;
    Quaternion rotation;
    Coroutine reduceCDCoroutine;

    void OnEnable()
    {
        currentFireCD = baseFireCD;
        timer = currentFireCD; // get ready for first shot
        overheatBar.gameObject.SetActive(true);

        // calculate elapsed time from the deactive moment to update heat bar
        float elapsedTime = Time.time - deactiveTime;
        currentHeatValue -= elapsedTime * overheatValue / cooldownTime;
        UpdateOverheatBar(currentHeatValue / overheatValue);
    }

    void Update()
    {
        Cooldown();

        if (!isShooting || overheated) return;

        if (timer < currentFireCD)
        {
            timer += Time.deltaTime;
        }

        CheckTimer();
        CheckOverheat();
    }

    void CheckOverheat()
    {
        if (currentHeatValue >= overheatValue)
        {
            overheated = true;
            cooldownPower = 1f;
        }
    }

    void Cooldown()
    {
        // cooldown power is 1 mean the gun is overheat so no need to calculate cooldown power
        if (!Mathf.Approximately(cooldownPower, 1f)) {
            // calculate cooldown power base on current heat value, the higher heat value, the faster cooldown
            cooldownPower = Mathf.Max(minCooldownPower, currentHeatValue / overheatValue);
        }

        // cooldown base on cooldown time, power and overheat value
        currentHeatValue -= Time.deltaTime * (overheatValue / cooldownTime) * cooldownPower;
        currentHeatValue = Mathf.Max(currentHeatValue, 0f); // keep it higher than 0f
        UpdateOverheatBar(currentHeatValue / overheatValue);

        // turn off overheat
        if (currentHeatValue <= 0f)
        {
            overheated = false;
            cooldownPower = minCooldownPower;
        }
    }

    void CheckTimer()
    {
        if (timer >= currentFireCD)
        {
            timer = 0f;
            Shoot(rotation);
            currentHeatValue += heatPerShot; // increase overheat bar per shot
            UpdateOverheatBar(currentHeatValue / overheatValue);
        }
    }

    void UpdateOverheatBar(float percent)
    {
        overheatBar.value = percent;
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

    void OnDisable()
    {
        deactiveTime = Time.time;
        
        overheatBar.gameObject?.SetActive(false);
    }
}
