using UnityEngine;

public class PlayerCollisionHandler : HitHanlder, IHitByEnemy, IExplosionTrigger
{
    protected new void Start() {
        base.Start();
        health?.AddEventOnHealthReachZero(_ => Die());
    }

    public void Hit(float _dmg) {
        health?.DecreaseHealth(_dmg);
    }

    public void Die() {
        Debug.Log("player is death");
    }
}