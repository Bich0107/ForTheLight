using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField] List<Attack> attackList;
    [SerializeField] Attack startAttack;
    [SerializeField] Attack finalAttack;
    [SerializeField] Attack currentAttack;
    [Tooltip("Wait time before starting to attack")]
    [SerializeField] float waitTime;
    [Tooltip("When boss's HP reach this percent, start charging beam attack and spawn minions")]
    [SerializeField] float hitPointPercent;
    [SerializeField] AudioClip slashSFX;
    [SerializeField] bool isAttacking = false;
    [SerializeField] float deathDelay;
    bool enableByPool = true;
    bool disableByPool = true;
    bool finalAttackState = false;

    protected new void OnEnable()
    {
        base.OnEnable();

        if (enableByPool)
        {
            enableByPool = false;
            return;
        }

        // ensure the boss is on the same height with player
        Vector3 position = transform.position;
        position.y = FindObjectOfType<Player>().transform.position.y;
        transform.position = position;

        healthController.AddEventOnHealthReachZero(_ =>
        {
            // prevent the boss from being killed before enter final stage
            if (!finalAttackState)
            {
                EnterFinalState();
            }
            else
            {
                StartCoroutine(CR_Death());
            }
        });

        foreach (Attack attack in attackList)
        {
            attack.Initialize(player);
        }
        finalAttack.Initialize(player);

        StartCoroutine(CR_Wait());
    }

    IEnumerator CR_Wait()
    {
        isAttacking = true;
        yield return new WaitForSeconds(waitTime);
        yield return StartCoroutine(startAttack.StartAttack());
        currentAttack = startAttack;
        startAttack.Reset();

        isAttacking = false;
    }

    void Update()
    {
        if (healthController.GetHealthPercent > hitPointPercent && !finalAttackState)
        {
            ProcessAttacks();
        }
        else
        {
            EnterFinalState();
        }
    }

    void ProcessAttacks()
    {
        if (isAttacking) return;
        isAttacking = true;
        StartCoroutine(CR_ChooseRandomAttack());
    }

    IEnumerator CR_ChooseRandomAttack()
    {
        int num = Random.Range(0, attackList.Count);
        yield return StartCoroutine(attackList[num].StartAttack());
        currentAttack = attackList[num];
        attackList[num].Reset();
        isAttacking = false;
    }

    void EnterFinalState()
    {
        if (finalAttackState) return;
        
        finalAttackState = true;
        currentAttack.Reset();
        isAttacking = true;
        StartCoroutine(finalAttack.StartAttack());
    }

    public void PlaySlashSound()
    {
        AudioManager.Instance.PlaySound(slashSFX);
    }

    IEnumerator CR_Death()
    {
        deathVFX.SetActive(true);

        disableByPool = true;
        enableByPool = true;

        yield return new WaitForSeconds(deathDelay);
        SaveManager.Win = true;
        GameManager.Instance.GameOver();
    }

    public override void Reset()
    {
        if (disableByPool)
        {
            disableByPool = false;
            return;
        }

        base.Reset();

        foreach (Attack attack in attackList)
        {
            attack.Reset();
        }

        finalAttackState = false;
        finalAttack.Reset();
    }
}
