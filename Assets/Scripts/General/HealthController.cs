using System;
using System.Collections;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    bool isCheated;
    [SerializeField] float maxHealth;
    [SerializeField] float health;
    [SerializeField] float invincibleTime = 0f;
    public float GetHealth => health;
    public float GetMaxHealth => maxHealth;
    public float GetHealthPercent => Mathf.Round(health / maxHealth * 100f);

    Action<object> onHealthReachZero;
    Action<object> onHit;

    bool isInvincible = false;

    void OnEnable()
    {
        health = maxHealth;
        isInvincible = false;
    }

    IEnumerator CR_TurnInvincible()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibleTime);
        isInvincible = false;
    }

    public void DecreaseHealth(float _value)
    {
        if (isCheated || isInvincible) return;

        StartCoroutine(CR_TurnInvincible());

        health -= _value;

        onHit?.Invoke(null);

        // check when hp = 0
        if (health <= Mathf.Epsilon)
        {
            onHealthReachZero?.Invoke(null);
        }
    }

    public void IncreaseHealth(float _value)
    {
        health = Mathf.Min(health + _value, maxHealth);
    }

    public void Reset()
    {
        health = maxHealth;
        ResetOnHealthReachZeroEvent();
    }

    void OnDisable()
    {
        Reset();
    }

    public void ToggleCheat()
    {
        isCheated = !isCheated;
        Debug.Log("invincible " + (isCheated ? "on" : "off"));
    }

    public void AddEventOnHit(Action<object> _action)
    {
        onHit += _action;
    }

    public void AddEventOnHealthReachZero(Action<object> _action)
    {
        onHealthReachZero += _action;
    }

    public void ResetOnHealthReachZeroEvent()
    {
        onHealthReachZero = null;
    }
}
