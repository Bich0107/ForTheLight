using System;
using UnityEngine;

public class HitHanlder : MonoBehaviour
{
    [SerializeField] protected LightObject lightObject;
    [SerializeField] protected HealthController health;

    protected void Start() {
        lightObject = GetComponent<LightObject>();
        health = GetComponent<HealthController>();
        
        if (lightObject != null && health != null) {
            health.AddEventOnHit((object _obj) => lightObject.Blink());
        }
    }
}