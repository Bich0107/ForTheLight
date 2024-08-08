using System;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] float maxHealth;
    [SerializeField] float health;
    public float GetHealth => health;
    public float GetHealthPercent => Mathf.Round(health / maxHealth * 100f);

    Action<object> onHealthReachZero;
    Action<object> onHit;

    private void Start() {
        health = maxHealth;
    }

    public void DecreaseHealth(float _value) {
        health -= _value;

        onHit?.Invoke(null);

        // check when hp = 0
        if (health <= Mathf.Epsilon) {
            onHealthReachZero?.Invoke(null);
        }
    }

    public void IncreaseHealth(float _value) {
        health = Mathf.Min(health + _value, maxHealth);
    }

    public void Reset() {
        health = maxHealth;
    }

    public void AddEventOnHit(Action<object> _action) {
        onHit += _action;
    }

    public void AddEventOnHealthReachZero(Action<object> _action) {
        onHealthReachZero += _action;
    }
}
