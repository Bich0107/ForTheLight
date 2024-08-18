using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplayer : MonoBehaviour
{
    [SerializeField] HealthController health;
    [SerializeField] Slider healthSlider;
    [SerializeField] float slideSpeed;

    void OnEnable()
    {
        Reset();
    }

    public void UpdateHealth(float _value) {
        float value = Mathf.Clamp(_value, 0f, health.GetMaxHealth);
        StopAllCoroutines();
        StartCoroutine(CR_UpdateHealth(value));
    }

    IEnumerator CR_UpdateHealth(float _targetValue) {
        float startValue = healthSlider.value;

        float tick = 0f;
        while (!Mathf.Approximately(healthSlider.value, _targetValue)) {
            tick += Time.deltaTime;
            healthSlider.value = Mathf.Lerp(startValue, _targetValue, slideSpeed * tick);
            yield return null;
        }
    }

    public void Reset()
    {
        StopAllCoroutines();
        healthSlider.maxValue = health.GetMaxHealth;
        healthSlider.value = health.GetMaxHealth;
    }
}
