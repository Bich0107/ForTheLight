using System;
using UnityEngine;

public class EnemyCollisionHandler : HitHanlder
{
    [SerializeField] Enemy enemyScript;
    [SerializeField] float collideDamage;
    [SerializeField] bool selfDestructOnTrigger = false;

    protected override void Start() {
        base.Start();
        enemyScript = GetComponent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (enemyScript.IsDead) return;
        
        IHitByEnemy hit = other.GetComponent<IHitByEnemy>();
        if (hit != null) {
            hit.Hit(collideDamage);
            if (selfDestructOnTrigger) gameObject.SetActive(false);
        }
    }
}
