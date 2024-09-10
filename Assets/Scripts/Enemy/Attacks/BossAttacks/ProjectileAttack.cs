using System.Collections;
using UnityEngine;

public class ProjectileAttack : Attack
{
    [Header("Projectile attack settings")]
    [SerializeField] ProjectileSO projectileScript;
    [SerializeField] Vector3 spawnPos;
    [SerializeField] AudioClip attackSFX;
    [SerializeField] GameObject target;
    [Tooltip("Max distance from spawn position to real spawn position")]
    [SerializeField] float range;
    [SerializeField] int minAttackCount;
    [SerializeField] int maxAttackCount;
    int amount;

    public override void Initialize(GameObject _target)
    {
        target = _target;
    }

    public override IEnumerator Start()
    {
        amount = GetAttackAmount();

        for (int i = 0; i < amount; i++)
        {
            GameObject g = ObjectPool.Instance.Spawn(projectileScript.Projectile.tag);
            if (g == null)
            {
                Debug.LogWarning("Projectile null");
                yield break;
            }

            g.transform.position = GetSpawnPos(spawnPos, range);
            g.SetActive(true);
            g.GetComponent<Projectile>().Fire(GetTargetDirection(g.transform.position));
            AudioManager.Instance.PlaySound(attackSFX);

            yield return new WaitForSeconds(projectileScript.GetAttackDelay);
        }
    }

    public override void Reset() {}

    int GetAttackAmount() {
        return Random.Range(minAttackCount, maxAttackCount + 1);
    }

    Vector2 GetSpawnPos(Vector2 spawnPos, float range)
    {
        Vector2 result = spawnPos + Random.insideUnitCircle.normalized * range;
        return result;
    }

    Vector2 GetTargetDirection(Vector3 startPos)
    {
        return (target.transform.position - startPos).normalized;
    }
}
