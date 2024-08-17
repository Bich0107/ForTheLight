using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField] Animator animator;
    [SerializeField] EnemyAttackController projectileAttackController;
    [Tooltip("Wait time before starting to attack")]
    [SerializeField] float waitTime;

    [Header("Jump attack settings")]
    float maxHeight;
    float minHeight;
    [SerializeField] float jumpHeight = 30f;
    [SerializeField] float jumpSpeed;
    [SerializeField] float dropDelay;
    [SerializeField] float jumpDelay;
    [SerializeField] float jumpCount;
    [SerializeField] GameObject groundHitVFX;
    [SerializeField] AudioClip groundHitSFX;

    [Header("Stab attack settings")]
    [SerializeField] float attackDistance;
    [SerializeField] float moveSpeed;
    [SerializeField] AudioClip slashSFX;

    [Header("Final beam attack settings")]
    [SerializeField] Vector2 attackPos;
    [Tooltip("When boss's HP reach this percent, start charging beam attack and spawn minions")]
    [SerializeField] float hitPointPercent;
    [SerializeField] EnemySpawner spawner;
    [SerializeField] List<EnemyWaveSO> minionWaves;
    [SerializeField] GameObject chargeVFX;
    [SerializeField] GameObject beamVFX;
    [SerializeField] float chargeTime;
    [SerializeField] float healthRestorePercent = 35f;

    [Header("SFX settings")]
    [SerializeField] AudioClip chargeSFX;
    [SerializeField] float chargeSFXInterval;
    [SerializeField] AudioClip beamFireSFX;
    Coroutine chargeSFXCoroutine;

    bool projectileReady;
    bool bodyReady = true;
    bool isAttacking = false;
    bool finalAttackStage = false;
    Coroutine attackCoroutine;

    protected new void OnEnable()
    {
        base.OnEnable();

        // ensure the boss is on the same height with player
        Vector3 position = transform.position;
        position.y = FindObjectOfType<Player>().transform.position.y;
        transform.position = position;

        minHeight = transform.position.y;
        maxHeight = minHeight + jumpHeight;

        spawner = FindObjectOfType<EnemySpawner>();
        projectileAttackController = GetComponent<EnemyAttackController>();
        animator = GetComponent<Animator>();
        healthController.AddEventOnHealthReachZero(_ =>
        {
            SaveManager.Win = true;
            GameManager.Instance.GameOver();
        });

        StartCoroutine(CR_Wait());
    }

    IEnumerator CR_Wait()
    {
        yield return new WaitForSeconds(waitTime);
        isAttacking = true;

        attackCoroutine = StartCoroutine(CR_JumpAttack());
    }

    void Update()
    {
        if (!isAttacking) return;

        if (healthController.GetHealthPercent > hitPointPercent)
        {
            ProcessAttacks();
        }
        else
        {
            EnterFinalStage();
        }
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
    void EnterFinalStage()
    {
        if (finalAttackStage) return;
        finalAttackStage = true;

        // restore health
        healthController.IncreaseHealth(healthRestorePercent / 100 * healthController.GetMaxHealth);

        Flip(player.transform.position.x > transform.position.x);
        projectileAttackController.Stop();
        if (attackCoroutine != null) StopCoroutine(attackCoroutine);

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

        chargeSFXCoroutine = StartCoroutine(CR_PlayChargeSFX());

        StartSpawningMinions();
        StartCoroutine(CR_ChargeBeamAttack());
    }

    IEnumerator CR_PlayChargeSFX() {
        do {
            AudioManager.Instance.PlaySound(chargeSFX);
            yield return new WaitForSeconds(chargeSFXInterval);
        } while (true);
    }

    IEnumerator CR_ChargeBeamAttack()
    {
        chargeVFX.SetActive(true);
        yield return new WaitForSeconds(chargeTime);
        StopCoroutine(chargeSFXCoroutine);
        Flip(player.transform.position.x > transform.position.x);

        AudioManager.Instance.PlaySound(beamFireSFX);
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
            groundHitVFX.SetActive(false);
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
            groundHitVFX.SetActive(true);
            moveController.Stop();
            AudioManager.Instance.PlaySound(groundHitSFX);

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

    public void PlaySlashSound()
    {
        AudioManager.Instance.PlaySound(slashSFX);
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

    public override void Reset()
    {
        base.Reset();
        StopAllCoroutines();
        isAttacking = false;
        bodyReady = true;
        finalAttackStage = false;
        
        chargeVFX.SetActive(false);
        beamVFX.SetActive(false);
    }
}
