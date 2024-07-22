using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    [SerializeField] Enemy enemy;
    [Space]
    [SerializeField] List<EnemyAttackSO> attackList;
    [SerializeField] Transform projectileSpawnPos;
    [SerializeField] bool attacking;
    Coroutine attackCoroutine;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    IEnumerator CR_Attack()
    {
        do
        {
            EnemyAttackSO attack = GetRandomAttack();
            yield return new WaitForSeconds(attack.GetAttackDelay());

            GameObject g = ObjectPool.Instance.Spawn(attack.GetProjectile.tag);
            if (g == null) {
                Debug.LogWarning("Projectile null");
                yield break;
            }
            g.transform.position = projectileSpawnPos.position;
            g.SetActive(true);
            
            g.GetComponent<Projectile>().Fire(GetTargetDirection());

            yield return new WaitForSeconds(attack.GetCooldown());
        } while (attacking);
    }

    public void Attack()
    {
        if (attacking) return;

        attacking = true;
        attackCoroutine = StartCoroutine(CR_Attack());
    }

    public void Stop()
    {
        if (!attacking) return;
        
        StopCoroutine(attackCoroutine);
        attacking = false;
    }

    public void Reset() {
        attacking = false;
        StopAllCoroutines();
        attackCoroutine = null;
    }

    EnemyAttackSO GetRandomAttack()
    {
        int index = Random.Range(0, attackList.Count);
        return attackList[index];
    }

    Vector2 GetTargetDirection()
    {
        return (enemy.GetPlayer.transform.position - projectileSpawnPos.position).normalized;
    }
}
