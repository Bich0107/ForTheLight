using System.Collections;
using UnityEngine;

public class LightSnatcher : Enemy
{
    [SerializeField] protected float attackDistance;
    [SerializeField] protected float chargeTime;
    [SerializeField] protected float chargeSpeedFactor;
    protected bool isAttacking = false;
    protected Coroutine attackCoroutine;

    protected new void OnEnable() {
        base.OnEnable();
    }

    protected void Update() {
        if (isAttacking) return;
        MoveTorwardTarget();    
    }

    protected void MoveTorwardTarget() {
        if (GetDistanceToPlayer > attackDistance) {
            moveController.Move(GetDirectionToPlayer);
        } else {
            isAttacking = true;
            Attack();
        }
    }

    protected virtual void Attack() {
        attackCoroutine = StartCoroutine(CR_Attack());
    }

    protected virtual IEnumerator CR_Attack() {
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
