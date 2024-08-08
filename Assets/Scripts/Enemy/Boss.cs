using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField] Animator animator;
    [SerializeField] EnemyAttackController projectileAttackController;
    [Tooltip("Wait time before starting to attack")]
    [SerializeField] float waitTime;

    [Header("Jump attack settings")]
    [SerializeField] float maxHeight;
    [SerializeField] float minHeight;
    [SerializeField] float jumpSpeed;
    [SerializeField] float dropDelay;
    [SerializeField] float jumpDelay;
    [SerializeField] float jumpCount;

    [Header("Stab attack settings")]
    [SerializeField] float attackDistance;
    [SerializeField] float moveSpeed;

    [Header("Final beam attack settings")]
    [Tooltip("When boss's HP reach this percent, start charging beam attack and spawn minions")]
    [SerializeField] float hitPointPercent;
    [SerializeField] EnemySpawner spawner;
    [SerializeField] List<EnemyWaveSO> minionWaves;
    [SerializeField] ParticleSystem chargeVFX;
    [SerializeField] ParticleSystem miniBeamVFX;
    [SerializeField] ParticleSystem beamVFX;
    [SerializeField] float chargeTime;

    bool projectileReady;
    bool bodyReady = true;
    bool finalAttackStage = false;
    Coroutine attackCoroutine;

    protected new void OnEnable()
    {
        base.OnEnable();
        projectileAttackController = GetComponent<EnemyAttackController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        ProcessAttacks();
        FinalAttack();
    }

    void FinalAttack()
    {
        if (healthController.GetHealthPercent > hitPointPercent) return;

        EnterFinalStage();
        StartSpawningMinions();
        StartCoroutine(ChargeBeamAttack());
        StartCoroutine(MiniBeamAttack());
    }

    void StartSpawningMinions()
    {
        spawner.SetWaves(minionWaves, true);
        spawner.StartSpawning();
    }

    IEnumerator ChargeBeamAttack()
    {
        chargeVFX.Play();
        yield return new WaitForSeconds(chargeTime);
        beamVFX.Play();
    }

    IEnumerator MiniBeamAttack()
    {
        miniBeamVFX.Play();
        yield return new WaitForSeconds(chargeTime);
        miniBeamVFX.Stop();
    }

    void ProcessAttacks()
    {
        if (finalAttackStage) return;

        projectileReady = !projectileAttackController.IsAttacking;

        // choose random between attack with projectile or attack with body
        if (projectileReady && bodyReady)
        {
            int num = Random.Range(0, 100);
            if (num < 50)
            {
                projectileAttackController.Attack(false);
            }
            else
            {
                ChooseRandomAttack();
            }
        }
    }

    void ChooseRandomAttack()
    {
        int num = Random.Range(0, 2);
        switch (num)
        {
            case 0:
                JumpAttack();
                break;
            case 1:
                SlashingAttack();
                break;
        }
    }

    void JumpAttack()
    {
        attackCoroutine = StartCoroutine(CR_JumpAttack());
    }

    IEnumerator CR_JumpAttack()
    {
        bodyReady = false;

        for (int i = 0; i < jumpCount; i++)
        {
            moveController.MoveSpeed = jumpSpeed;
            moveController.Move(Vector2.up);

            // wait until reaching maximum height
            while (transform.position.y <= maxHeight) yield return null;
            moveController.Stop();

            transform.position = new Vector3(player.transform.position.x, transform.position.y);
            yield return new WaitForSeconds(dropDelay);
            moveController.MoveSpeed = jumpSpeed;
            moveController.Move(Vector2.down);

            // wait until reaching minimum height
            while (transform.position.y >= minHeight) yield return null;
            moveController.Stop();

            yield return new WaitForSeconds(jumpDelay);
        }

        bodyReady = true;
    }

    void SlashingAttack()
    {
        attackCoroutine = StartCoroutine(CR_SlashingAttack());
    }

    IEnumerator CR_SlashingAttack()
    {
        bodyReady = false;

        // move until player is in attack range
        moveController.MoveSpeed = moveSpeed;
        moveController.Move(Vector2.right * GetDirectionToPlayer.x);

        while (GetHorizontalDistanceToPlayer > attackDistance) yield return null;
        moveController.Stop();

        // flip base on the position of player to the boss
        Flip(player.transform.position.x > transform.position.x);

        animator.SetTrigger("slashAttack");
    }

    void EnterFinalStage()
    {
        if (finalAttackStage) return;

        projectileAttackController.Stop();
        if (attackCoroutine != null) StopCoroutine(attackCoroutine);
        finalAttackStage = true;
    }

    void Flip(bool _left)
    {
        transform.localScale = new Vector3(_left ? -1f : 1f, transform.localScale.y, transform.localScale.z);
    }

    public void SlashAttackEnd()
    {
        bodyReady = true;
        attackCoroutine = null;
    }
}
