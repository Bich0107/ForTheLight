using System;
using UnityEngine;

public class EnemyCollisionHandler : HitHanlder
{
    [SerializeField] float collideDamage;
    [SerializeField] Action<object> onTrigger;

    private void OnTriggerEnter2D(Collider2D other) {
        IHitByEnemy hit = other.GetComponent<IHitByEnemy>();
        if (hit != null) {
            hit.Hit(collideDamage);
            onTrigger?.Invoke(collideDamage);
            Destroy(gameObject);
        }
    }

    public void AddOnTriggerEvent(Action<object> _action) {
        onTrigger += _action;
    }
}
