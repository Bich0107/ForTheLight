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
    [Tooltip("Distance from real spawn pos to the spawn pos, direction is random")]
    [SerializeField] float distance;

    public GameObject GetProjectile => projectile;
    public float GetDistance() => distance;
    public float GetAttackDelay() => attackDelay;
    public float GetCooldown() => cd;
    public int GetAmount() => amount;
}