using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Attack", menuName = "Enemy Attack")]
public class EnemyAttackSO : ScriptableObject {
    [SerializeField] GameObject projectile;
    [Tooltip("Delay time between shot")]
    [SerializeField] float attackDelay;
    [Tooltip("Wait time after the attack end")]
    [SerializeField] float cd;
    [Tooltip("The number of shot")]
    [SerializeField] int amount;

    public GameObject GetProjectile => projectile;
    public float GetAttackDelay() => attackDelay;
    public float GetCooldown() => cd;
    public int GetAmount() => amount;
}