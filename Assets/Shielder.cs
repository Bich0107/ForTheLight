using System.Collections;
using UnityEngine;

public class Shielder : Enemy
{
    [SerializeField] Shield shield;
    [SerializeField] Collider2D detectCollider; // use to choose one enemy to protect when spawn
    [SerializeField] Collider2D bodyCollider; // use to detect player's bullet after defense break
    [SerializeField] float delay;

    protected new void OnEnable()
    {
        base.OnEnable();
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

    private void OnTriggerEnter2D(Collider2D other) {
        // choose an enemy to protect here
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
