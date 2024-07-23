using System.Collections;
using UnityEngine;

public class ProjectileThrower : Enemy
{
    [SerializeField] EnemyAttackController attackController;
    [SerializeField] Vector3 offset;
    [SerializeField] float minDistanceToTarget = 5f;
    [SerializeField] float minDistanceToOffset = 1.5f;

    protected new void OnEnable() {
        base.OnEnable();
        attackController = GetComponent<EnemyAttackController>();
    }

    void FixedUpdate()
    {
        // not use yet
        //if (!GameManager.Instance.gameStarted) return;

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

    public new void Reset() {
        base.Reset();
        attackController?.Reset();
    }
}