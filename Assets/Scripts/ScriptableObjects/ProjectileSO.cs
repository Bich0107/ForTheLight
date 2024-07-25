using UnityEngine;

[CreateAssetMenu(menuName = "Projectile", fileName = "New projectile")]
public class ProjectileSO : ScriptableObject
{
    [Tooltip("Delay time before moving")]
    [SerializeField] float attackDelay = 0;
    [SerializeField] float moveSpeed;
    [SerializeField] float damage;

    
    public float GetMoveSpeed => moveSpeed;
    public float GetAttackDelay => attackDelay;
    public float GetDamage => damage;
}
