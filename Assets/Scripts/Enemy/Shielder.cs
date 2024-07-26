using System.Collections;
using UnityEngine;

public class Shielder : Enemy
{
    [SerializeField] Shield shield;
    [SerializeField] Collider2D detectCollider; // use to choose one enemy to protect when spawn
    [SerializeField] Collider2D bodyCollider; // use to detect player's bullet after defense break
    [SerializeField] float delay;
    [SerializeField] float distanceFromTarget;
    [SerializeField] float minDistanceToPos;
    [SerializeField] GameObject protectTarget;

    protected new void OnEnable()
    {
        base.OnEnable();
        bodyCollider.enabled = false;
        detectCollider.enabled = true;
        StartCoroutine(CR_Defense());
    }

    IEnumerator CR_Defense()
    {
        do
        {
            detectCollider.enabled = false;
            bodyCollider.enabled = false;
            yield return new WaitForSeconds(delay);
            shield.Defend();
        } while (true);
    }

    private void Update() {
        ProtectTarget();
    }

    void ProtectTarget() {
        if (protectTarget == null) return;
        Vector3 guardDirection = (player.transform.position - protectTarget.transform.position).normalized;
        Vector3 position = protectTarget.transform.position + guardDirection * distanceFromTarget;
        Vector3 directionToPos = (position - transform.position).normalized;

        if (Vector3.Distance(position, transform.position) > minDistanceToPos) {
            moveController.Move(directionToPos);
        } else moveController.Stop();
    }

    private void OnTriggerStay2D(Collider2D other) {
        // choose an enemy to protect here
        IProtectedByShielder target = other.GetComponent<IProtectedByShielder>();
        if (target != null && protectTarget == null) {
            protectTarget = other.gameObject;
            detectCollider.enabled = false;
        }
    }

    public void DefenseBreak() {
        bodyCollider.enabled = true;
    }

    public void Restore() {
        bodyCollider.enabled = false;
    }

    public new void Reset() {
        base.Reset();
        Restore();
    }
}
