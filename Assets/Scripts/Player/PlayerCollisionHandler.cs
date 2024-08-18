using UnityEngine;

public class PlayerCollisionHandler : HitHanlder, IHitByEnemy, IExplosionTrigger
{
    [SerializeField] HealthDisplayer healthDisplayer;
    bool isActive = true;

    protected new void Start()
    {
        base.Start();
    }

    public void ToggleActive()
    {
        isActive = !isActive;
    }

    public void Hit(float _dmg)
    {
        if (!isActive) return;

        health?.DecreaseHealth(_dmg);
        healthDisplayer.UpdateHealth(health.GetHealth);
    }
}