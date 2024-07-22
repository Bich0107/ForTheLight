using UnityEngine;

public class PlayerCollisionHandler : HitHanlder, IHitByEnemy
{
    protected new void Start() {
        base.Start();
    }

    public void Hit(float _dmg) {
        health?.DecreaseHealth(_dmg);
    }
}