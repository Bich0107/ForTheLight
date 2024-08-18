using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightAnimation : MonoBehaviour
{
    [SerializeField] Light2D light;
    [Header("Intensity")]
    [SerializeField] float startValue;
    [SerializeField] float endValue;
    [SerializeField] float changeTime;

    void OnEnable()
    {
        light.intensity = startValue;
    }

    public void Play()
    {
        Stop();
        StartCoroutine(CR_Change(startValue, endValue));
    }

    public void Rewind()
    {
        Stop();
        StartCoroutine(CR_Change(endValue, startValue));
    }

    public void Stop()
    {
        StopAllCoroutines();
    }

    void OnDisable()
    {
        Stop();
    }

    IEnumerator CR_Change(float _startValue, float _endValue)
    {
        float tick = 0f;

        while (!Mathf.Approximately(light.intensity, _endValue))
        {
            tick += Time.deltaTime;
            light.intensity = Mathf.Lerp(_startValue, _endValue, tick / changeTime);
            yield return null;
        }

        light.intensity = _endValue;
    }
}
