using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;

public class ProjectileThrower : Enemy, IProtectedByShielder
{
    [SerializeField] EnemyAttackController attackController;
    [SerializeField] Vector3 offset;
    [SerializeField] float minDistanceToTarget = 5f;
    [SerializeField] float minDistanceToOffset = 1.5f;
    bool isProtected = false;

    protected new void OnEnable()
    {
        base.OnEnable();
        offset = Vector3.zero;
        attackController = GetComponent<EnemyAttackController>();
        isProtected = false;
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

    public bool Protect()
    {
        if (isProtected) return false;
        isProtected = true;
        return true;
    }
}