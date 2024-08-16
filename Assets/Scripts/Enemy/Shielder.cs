using System.Collections;
using UnityEngine;

public class Shielder : Enemy
{
    [SerializeField] Shield shield;
    [SerializeField] Collider2D detectCollider; // use to choose one enemy to protect when spawn
    [SerializeField] Collider2D bodyCollider; // use to detect player's bullet after defense break
    [SerializeField] float distanceFromTarget;
    [SerializeField] float minDistanceToPos;
    [SerializeField] GameObject protectTarget;

    protected override void OnEnable()
    {
        base.OnEnable();
        bodyCollider.enabled = false;
        detectCollider.enabled = true;
    }

    void Update()
    {
        ProtectTarget();
    }

    void ProtectTarget()
    {
        if (protectTarget == null) return;

        Vector3 guardDirection = (player.transform.position - protectTarget.transform.position).normalized;
        Vector3 position = protectTarget.transform.position + guardDirection * distanceFromTarget;
        Vector3 directionToPos = (position - transform.position).normalized;

        if (Vector3.Distance(position, transform.position) > minDistanceToPos)
        {
            moveController.Move(directionToPos);
        }
        else moveController.Stop();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        // return if it already has a protect target
        if (protectTarget != null) return;

        // choose an enemy to protect here
        IProtectedByShielder target = other.GetComponent<IProtectedByShielder>();
        if (target != null && target.Protect())
        {
            protectTarget = other.gameObject;
            detectCollider.enabled = false;
            shield.Defend();
        }
    }

    public void DefenseBreak()
    {
        bodyCollider.enabled = true;
    }

    public void Restore()
    {
        bodyCollider.enabled = false;
    }

    public override void Reset()
    {
        base.Reset();
        protectTarget = null;
        StopAllCoroutines();
        Restore();
    }
}
