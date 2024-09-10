using System.Collections;
using UnityEngine;

public class SlashAttack : Attack
{
    [Header("GO and components")]
    [SerializeField] Enemy enemy;
    [SerializeField] MovementController moveController;
    [SerializeField] Animator animator;
    GameObject target;
    [Header("Settings")]
    [SerializeField] float attackDistance;
    [SerializeField] float moveSpeed;

    public override void Initialize(GameObject _target)
    {
        target = _target;
    }

    public override IEnumerator Start()
    {
        // move until player is in attack range
        moveController.MoveSpeed = moveSpeed;
        moveController.Move(Vector2.right * enemy.GetDirectionToPlayer.x);

        while (enemy.GetHorizontalDistanceToPlayer > attackDistance) yield return null;
        moveController.Stop();

        // flip base on the position of player to the boss
        Flip(target.transform.position.x > transform.position.x);

        animator.SetTrigger("slashAttack");
    }

    public override void Reset()
    {
        moveController.Reset();
    }

    void Flip(bool _left)
    {
        transform.localScale = new Vector3(_left ? -1f : 1f, transform.localScale.y, transform.localScale.z);
    }
}
