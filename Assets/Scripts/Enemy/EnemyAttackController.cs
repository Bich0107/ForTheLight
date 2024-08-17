using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    [SerializeField] Enemy enemy;
    [Space]
    [SerializeField] List<EnemyAttackSO> attackList;
    [SerializeField] Transform projectileSpawnPos;
    [SerializeField] List<AudioClip> attackSFXList;
    [SerializeField] AudioClip currentAttackSFX;
    [SerializeField] bool attacking;
    public bool IsAttacking => attacking;
    // if true, enemy will continue to attack until it dies.
    bool continuous;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    IEnumerator CR_Attack()
    {
        do
        {
            if (enemy.IsDead) yield break;
            
            EnemyAttackSO attack = GetRandomAttack();
            for (int i = 0; i < attack.GetAmount(); i++)
            {
                GameObject g = ObjectPool.Instance.Spawn(attack.GetProjectile.tag);
                if (g == null)
                {
                    Debug.LogWarning("Projectile null");
                    yield break;
                }

                g.transform.position = GetSpawnPos(projectileSpawnPos.position, attack.GetDistance());
                g.SetActive(true);
                g.GetComponent<Projectile>().Fire(GetTargetDirection(g.transform.position));
                AudioManager.Instance.PlaySound(currentAttackSFX);

                yield return new WaitForSeconds(attack.GetAttackDelay());
            }

            yield return new WaitForSeconds(attack.GetCooldown());

        } while (continuous);

        attacking = false;
    }

    public void Attack(bool _continuous = true)
    {
        if (attacking || continuous) return;

        continuous = _continuous;

        attacking = true;
        StartCoroutine(CR_Attack());
    }

    public void Stop()
    {
        if (!attacking && !continuous) return;

        Reset();
    }

    public void Reset()
    {
        attacking = false;
        continuous = false;
        StopAllCoroutines();
    }

    void OnDisable() {
        Reset();    
    }

    EnemyAttackSO GetRandomAttack()
    {
        int index = Random.Range(0, attackList.Count);
        currentAttackSFX = attackSFXList[index];
        return attackList[index];
    }

    Vector2 GetSpawnPos(Vector2 spawnPos, float range)
    {
        Vector2 result = spawnPos + Random.insideUnitCircle.normalized * range;
        return result;
    }

    Vector2 GetTargetDirection(Vector3 startPos)
    {
        return (enemy.GetPlayer.transform.position - startPos).normalized;
    }
}
