using System.Collections;
using UnityEngine;

public class LifeIcon : MonoBehaviour
{
    [SerializeField] ParticleSystem firePS;
    [SerializeField] float baseEmitRate;
    [SerializeField] float changeSpeed;
    bool isActive = true;

    public void TurnOn()
    {
        if (isActive) return;
        isActive = true;
        StartCoroutine(CR_ChangeEmitRate(baseEmitRate));
    }

    public void TurnOff()
    {
        if (!isActive) return;
        isActive = false;
        StartCoroutine(CR_ChangeEmitRate(0f));
    }

    IEnumerator CR_ChangeEmitRate(float finalValue)
    {
        ParticleSystem.EmissionModule emission = firePS.emission;
        ParticleSystem.MinMaxCurve emisstionRate = firePS.emission.rateOverTime;
        
        float startValue = emisstionRate.constant;
        float elapsedTime  = 0f;
        while (!Mathf.Approximately(startValue, finalValue))
        {
            elapsedTime  += Time.deltaTime;
            float newEmissionRate = Mathf.Lerp(startValue, finalValue, elapsedTime * changeSpeed);

            // set new emission rate
            var rate = emission.rateOverTime;
            rate.constant = newEmissionRate;
            emission.rateOverTime = rate;

            yield return null;
        }

        // Ensure the emission rate is set to the target value
        emisstionRate = emission.rateOverTime;
        emisstionRate.constant = finalValue;
        emission.rateOverTime = emisstionRate;
    }
}
