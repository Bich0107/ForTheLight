using System.Collections;
using UnityEngine;

public class LightSnatcher : Enemy
{
    [Header("Charge attack settings")]
    [SerializeField] protected float attackDistance;
    [SerializeField] protected float chargeTime;
    [SerializeField] protected float chargeSpeedFactor;
    [Header("Explode settings")]
    [SerializeField] Vector3 baseSize;
    [SerializeField] Vector3 explodeSize;
    [SerializeField] float explodeDelay;
    [SerializeField] float explodeDuration;
    [SerializeField] GameObject explodeVFX;
    
    protected bool isAttacking = false;
    protected Coroutine attackCoroutine;

    void Awake()
    {
        baseSize = transform.localScale;
    }

    protected new void OnEnable()
    {
        base.OnEnable();
        transform.localScale = baseSize;
    }

    protected void Update()
    {
        if (isAttacking) return;
        MoveTorwardTarget();
    }

    protected void MoveTorwardTarget()
    {
        if (GetDistanceToPlayer > attackDistance)
        {
            moveController.Move(GetDirectionToPlayer);
        }
        else
        {
            isAttacking = true;
            Attack();
        }
    }

    protected virtual void Attack()
    {
        attackCoroutine = StartCoroutine(CR_Attack());
    }

    protected virtual IEnumerator CR_Attack()
    {
        moveController.Stop();

        yield return new WaitForSeconds(chargeTime);

        moveController.ModifiyMoveSpeed(chargeSpeedFactor);
        moveController.Move(GetDirectionToPlayer);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        IExplosionTrigger trigger = other.GetComponent<IExplosionTrigger>();
        if (trigger != null) {
            Explode();
        }
    }

    protected override void Die() {
        Explode();
    }

    void Explode()
    {
        if (attackCoroutine != null) StopCoroutine(attackCoroutine);
        moveController.Stop();

        StartCoroutine(CR_Explode());
    }

    IEnumerator CR_Explode()
    {
        yield return new WaitForSeconds(explodeDelay);

        transform.localScale = explodeSize;
        explodeVFX.SetActive(true);

        yield return new WaitForSeconds(explodeDuration);
        explodeVFX.SetActive(false);
        gameObject.SetActive(false);
    }

    protected new void OnDisable()
    {
        base.OnDisable();

        Reset();
    }

    public new void Reset()
    {
        base.Reset();

        moveController.Reset();

        if (attackCoroutine != null) StopCoroutine(attackCoroutine);
        attackCoroutine = null;
        isAttacking = false;
    }
}
