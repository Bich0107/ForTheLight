using System.Collections;
using UnityEngine;

public class LightSnatcher : Enemy
{
    [SerializeField] float attackDistance;
    [SerializeField] float chargeTime;
    [SerializeField] float chargeSpeedFactor;
    [SerializeField] float selfDestructDelay;
    bool isAttacking = false;
    Coroutine attackCoroutine;

    protected new void OnEnable() {
        base.OnEnable();
    }

    void Update() {
        if (isAttacking) return;
        MoveTorwardTarget();    
    }

    void MoveTorwardTarget() {
        if (GetDistanceToPlayer > attackDistance) {
            moveController.Move(GetDirectionToPlayer);
        } else {
            isAttacking = true;
            Attack();
        }
    }

    void Attack() {
        attackCoroutine = StartCoroutine(CR_Attack());
    }

    IEnumerator CR_Attack() {
        moveController.Stop();
        
        yield return new WaitForSeconds(chargeTime);

        moveController.MoveSpeed = moveController.MoveSpeed * chargeSpeedFactor;
        moveController.Move(GetDirectionToPlayer);
    }

    protected new void OnDisable() {
        base.OnDisable();

        if (attackCoroutine != null) StopCoroutine(attackCoroutine);
        attackCoroutine = null;
        isAttacking = false;
    }

    public new void Reset() {
        base.Reset();

        if (attackCoroutine != null) StopCoroutine(attackCoroutine);
        attackCoroutine = null;
        isAttacking = false;
    }
}
