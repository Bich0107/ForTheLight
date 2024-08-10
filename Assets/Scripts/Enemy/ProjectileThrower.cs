using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;

public class ProjectileThrower : Enemy
{
    [SerializeField] EnemyAttackController attackController;
    [SerializeField] Vector3 offset;
    [SerializeField] float minDistanceToTarget = 5f;
    [SerializeField] float minDistanceToOffset = 1.5f;
    [Header("Death VFX settings")]
    [SerializeField] float effectDuration = 3f;
    [SerializeField] GameObject deathVFX;

    protected new void OnEnable()
    {
        base.OnEnable();
        attackController = GetComponent<EnemyAttackController>();
    }

    void FixedUpdate()
    {
        MoveTowardPlayer();
    }

    void MoveTowardPlayer()
    {
        // move toward player until reaching the minimum distance to player
        if (offset == Vector3.zero && GetDistanceToPlayer > minDistanceToTarget)
        {
            moveController.Move(GetDirectionToPlayer);
        }
        else
        {
            // keep that distance to player 
            if (offset == Vector3.zero)
                offset = transform.position - player.transform.position;
            else if (Vector2.Distance(player.transform.position + offset, transform.position) > minDistanceToOffset)
            {
                Vector2 direction = (player.transform.position + offset - transform.position).normalized;
                moveController.Move(direction);
            }
            else
            {   // attack when the player is in attack range
                attackController.Attack();
                moveController.Stop();
            }
        }
    }

    protected override void Die()
    {
        StartCoroutine(PlayDeathEffect());
    }

    IEnumerator PlayDeathEffect()
    {
        deathVFX?.SetActive(true);

        float tick = 0f;
        Vector3 baseScale = transform.localScale;
        while (tick <= effectDuration) {
            tick += Time.deltaTime;
            transform.localScale = Vector3.Lerp(baseScale, Vector3.zero, tick / effectDuration);
            yield return null;
        }

        deathVFX?.SetActive(false);
        transform.localScale = baseScale;
        gameObject.SetActive(false);
    }

    public new void Reset()
    {
        base.Reset();
        StopAllCoroutines();
        attackController?.Reset();
    }
}