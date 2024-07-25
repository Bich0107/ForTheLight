using System.Collections;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField] EnemyAttackController attackController;
    [Tooltip("Wait time before starting to attack")]
    [SerializeField] float waitTime;

    protected new void OnEnable()
    {
        base.OnEnable();
        attackController = GetComponent<EnemyAttackController>();
        Attack();
    }

    void Attack()
    {
        StartCoroutine(CR_Attack());
    }

    IEnumerator CR_Attack() {
        yield return new WaitForSeconds(waitTime);
        attackController.Attack();
    }
}
