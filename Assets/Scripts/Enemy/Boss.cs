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
    [SerializeField] Vector2 attackPos;
    [SerializeField] float hitPointPercent;
    [SerializeField] EnemySpawner spawner;
    [SerializeField] List<EnemyWaveSO> minionWaves;
    [SerializeField] GameObject chargeVFX;
    [SerializeField] GameObject beamVFX;
    [SerializeField] float chargeTime;
    [SerializeField] float healthRestorePercent = 35f;

    bool projectileReady;
    bool isAttacking = false;
    bool bodyReady = true;
    bool finalAttackStage = false;
    Coroutine attackCoroutine;

    protected new void OnEnable()
    {
        base.OnEnable();
        maxHeight += transform.position.y;
        minHeight += transform.position.y;

        spawner = FindObjectOfType<EnemySpawner>();
        projectileAttackController = GetComponent<EnemyAttackController>();
        animator = GetComponent<Animator>();

        StartCoroutine(CR_Wait());
    }

    IEnumerator CR_Wait()
    {
        yield return new WaitForSeconds(waitTime);
        isAttacking = true;
    }

    void Update()
    {
        if (!isAttacking) return;

        ProcessAttacks();
        FinalAttack();
    }

    void ProcessAttacks()
    {
        projectileReady = !projectileAttackController.IsAttacking;

        // choose random between attack with projectile or attack with body
        if (projectileReady && bodyReady && !finalAttackStage)
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

    #region Final atatck methods
    void FinalAttack()
    {
        if (healthController.GetHealthPercent > hitPointPercent) return;

        EnterFinalStage();
    }

    void EnterFinalStage()
    {
        if (finalAttackStage) return;

        // restore health
        healthController.IncreaseHealth(healthRestorePercent / 100 * healthController.GetMaxHealth);

        Flip(player.transform.position.x > transform.position.x);
        projectileAttackController.Stop();
        if (attackCoroutine != null) StopCoroutine(attackCoroutine);
        finalAttackStage = true;

        StartCoroutine(CR_FinalAttackSequence());
    }

    void StartSpawningMinions()
    {
        spawner.SetWaves(minionWaves, true);
        spawner.StartSpawning();
    }

    IEnumerator CR_FinalAttackSequence()
    {
        moveController.MoveSpeed = jumpSpeed;
        moveController.Move(Vector2.up);

        // wait until reaching maximum height
        while (transform.position.y <= maxHeight) yield return null;
        moveController.Stop();

        transform.position = new Vector3(attackPos.x, transform.position.y);

        moveController.MoveSpeed = jumpSpeed;
        moveController.Move(Vector2.down);

        // wait until reaching minimum height
        while (transform.position.y >= minHeight) yield return null;
        moveController.Stop();

        StartSpawningMinions();
        StartCoroutine(CR_ChargeBeamAttack());
    }

    IEnumerator CR_ChargeBeamAttack()
    {
        chargeVFX.SetActive(true);
        yield return new WaitForSeconds(chargeTime);
        chargeVFX.SetActive(false);
        beamVFX.SetActive(true);
    }
    #endregion

    #region Jump attack methods
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
    #endregion

    #region Slash attack methods
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

    void Flip(bool _left)
    {
        transform.localScale = new Vector3(_left ? -1f : 1f, transform.localScale.y, transform.localScale.z);
    }

    public void SlashAttackEnd()
    {
        bodyReady = true;
        attackCoroutine = null;
    }
    #endregion
}
