using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightObject : MonoBehaviour
{
    [SerializeField] new Light2D light;
    [Space]
    [Tooltip("Intensity value of this object will loop through this list")]
    [SerializeField] List<float> intensityList;
    [SerializeField] float intensityChangeRate;
    [Space]
    [Tooltip("Falloff value of this object will loop through this list")]
    [SerializeField] List<float> falloffList;
    [SerializeField] float falloffChangeRate;
    [Space]
    [SerializeField] float blinkDuration;
    [Tooltip("Delay time between each blink")]
    [SerializeField] float blinkDelay;
    [SerializeField] bool loop;
    Coroutine falloffCoroutine;
    Coroutine intensityCoroutine;

    public void Blink(object _obj = null)
    {
        if (falloffCoroutine != null) StopBlinkFalloff();
        if (intensityCoroutine != null) StopBlinkIntensity();

        falloffCoroutine = StartCoroutine(CR_BlinkFalloff());
        intensityCoroutine = StartCoroutine(CR_BlinkIntensity());
    }

    IEnumerator CR_BlinkIntensity()
    {
        if (intensityList.Count == 0) yield break;

        float timer = 0;
        float tick = 0;
        int index = 0;
        float targetValue = intensityList[index];
        do
        {
            timer += Time.deltaTime;
            tick += Time.deltaTime;

            light.intensity = Mathf.Lerp(light.intensity, targetValue, tick * intensityChangeRate);

            if (Mathf.Approximately(light.intensity, targetValue))
            {
                index = (index + 1) % intensityList.Count;
                targetValue = intensityList[index];
                tick = 0;
            }

            yield return new WaitForSeconds(blinkDelay);
        } while (loop || timer <= blinkDuration || !Mathf.Approximately(light.intensity, intensityList[0]));
    }

    IEnumerator CR_BlinkFalloff()
    {
        if (falloffList.Count == 0) yield break;

        float timer = 0;
        float tick = 0;
        int index = 0;
        float targetValue = falloffList[index];
        do
        {
            timer += Time.deltaTime;
            tick += Time.deltaTime;

            light.falloffIntensity = Mathf.Lerp(light.falloffIntensity, targetValue, tick * falloffChangeRate);

            if (Mathf.Approximately(light.falloffIntensity, targetValue))
            {
                index = (index + 1) % falloffList.Count;
                targetValue = falloffList[index];
                tick = 0;
            }

            yield return new WaitForSeconds(blinkDelay);
        } while (loop || timer <= blinkDuration || !Mathf.Approximately(light.falloffIntensity, falloffList[0]));
    }

    public void StopBlinkIntensity() => StopCoroutine(intensityCoroutine);
    public void StopBlinkFalloff() => StopCoroutine(falloffCoroutine);
    public void StopAll()
    {
        StopBlinkIntensity();
        StopBlinkFalloff();
    }
}