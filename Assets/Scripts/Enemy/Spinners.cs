using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinners : LightSnatcher
{
    [SerializeField] List<ParticleSystem> chargeVFXs;
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

    protected override void Attack()
    {
        attackCoroutine = StartCoroutine(CR_Attack());
    }

    protected override IEnumerator CR_Attack()
    {
        moveController.Stop();
        foreach (ParticleSystem particleSystem in chargeVFXs)
        {
            particleSystem.Play();
        }
        yield return new WaitForSeconds(chargeTime);

        rotateObject.RotateSpeed *= chargeSpeedFactor;
        moveController.ModifiyMoveSpeed(chargeSpeedFactor);
        moveController.Move(GetDirectionToPlayer);
        AudioManager.Instance.PlaySound(attackSFX);

        yield return new WaitForSeconds(chargeAttackDuration);
        foreach (ParticleSystem particleSystem in chargeVFXs)
        {
            particleSystem.Stop();
        }
        Recharge();
    }

    // to clear the ontrigger of parent class
    protected override void OnTriggerEnter2D(Collider2D other) { }

    void Recharge()
    {
        isAttacking = false;

        rotateObject.RotateSpeed /= chargeSpeedFactor;

        moveController.Reset();
        moveController.Move(GetDirectionToPlayer);
    }
}
