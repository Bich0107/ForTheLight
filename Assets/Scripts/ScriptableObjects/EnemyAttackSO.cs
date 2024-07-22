using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Attack", menuName = "Enemy Attack")]
public class EnemyAttackSO : ScriptableObject {
    [SerializeField] GameObject projectile;
    [SerializeField] float attackDelay;
    [SerializeField] float cd;

    public GameObject GetProjectile => projectile;
    public float GetAttackDelay() => attackDelay;
    public float GetCooldown() => cd;
}