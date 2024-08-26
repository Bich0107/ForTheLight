using System.Collections;
using UnityEngine;

public class SlashAttack : Attack
{
    [Header("GO and components")]
    [SerializeField] Enemy enemy;
    [SerializeField] GameObject target;
    [SerializeField] MovementController moveController;
    [SerializeField] Animator animator;
    [Header("Settings")]
    [SerializeField] float attackDistance;
    [SerializeField] float moveSpeed;
    [SerializeField] AudioClip slashSFX;

    public override void Initialize()
    {
        target = FindObjectOfType<Player>().gameObject;
        moveController = GetComponentInParent<MovementController>();
        animator = GetComponentInParent<Animator>();
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

    public void PlaySlashSound()
    {
        AudioManager.Instance.PlaySound(slashSFX);
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
