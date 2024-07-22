using System.Collections;
using UnityEngine;

public class Spinners : LightSnatcher
{
    [SerializeField] float chargeRotateSpdFactor;
    [SerializeField] float chargeAttackDuration;
    [SerializeField] RotateObject rotateObject;

    protected new void OnEnable()
    {
        base.OnEnable();
        rotateObject = GetComponent<RotateObject>();

        rotateObject?.Rotate();
    }

    protected new void Update()
    {
        base.Update();
    }

    protected override void Attack() {
        attackCoroutine = StartCoroutine(CR_Attack());
    }

    protected override IEnumerator CR_Attack()
    {
        moveController.Stop();

        yield return new WaitForSeconds(chargeTime);

        rotateObject.RotateSpeed *= chargeSpeedFactor;
        moveController.MoveSpeed *= chargeSpeedFactor;
        moveController.Move(GetDirectionToPlayer);

        yield return new WaitForSeconds(chargeAttackDuration);
        Recharge();
    }

    void Recharge()
    {
        isAttacking = false;

        moveController.Stop();
        rotateObject.RotateSpeed /= chargeSpeedFactor;
        moveController.MoveSpeed /= chargeSpeedFactor;
        moveController.Move(GetDirectionToPlayer);
    }
}
