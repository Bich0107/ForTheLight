using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField] List<Attack> attackList;
    [SerializeField] Attack finalAttack;
    [Tooltip("Wait time before starting to attack")]
    [SerializeField] float waitTime;
    [Tooltip("When boss's HP reach this percent, start charging beam attack and spawn minions")]
    [SerializeField] float hitPointPercent;
    [SerializeField] AudioClip slashSFX;
    bool isAttacking = false;
    bool enableByPool = true;

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
            SaveManager.Win = true;
            GameManager.Instance.GameOver();
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
        yield return new WaitForSeconds(waitTime);
        isAttacking = true;

        yield return StartCoroutine(attackList[0].Start());
        attackList[0].Reset();
        isAttacking = false;
    }

    void Update()
    {
        if (healthController.GetHealthPercent > hitPointPercent)
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

        StartCoroutine(CR_ChooseRandomAttack());
    }

    IEnumerator CR_ChooseRandomAttack()
    {
        isAttacking = true;
        int num = Random.Range(0, attackList.Count);
        Debug.Log("start attack number " + num);
        yield return StartCoroutine(attackList[num].Start());
        attackList[num].Reset();
        isAttacking = false;
    }

    void EnterFinalState()
    {
        if (isAttacking) return;
        Debug.Log("start final attack");
        isAttacking = true;
        StartCoroutine(finalAttack.Start());
    }

    public void PlaySlashSound()
    {
        AudioManager.Instance.PlaySound(slashSFX);
    }
}
