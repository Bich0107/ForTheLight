using UnityEngine;

[CreateAssetMenu(menuName = "Projectile", fileName = "New projectile")]
public class ProjectileSO : ScriptableObject
{
    [Tooltip("Delay time before moving")]
    [SerializeField] GameObject projectile;
    [SerializeField] float attackDelay = 0;
    [SerializeField] float moveSpeed;
    [SerializeField] float damage;
    
    public GameObject Projectile => projectile;
    public float GetMoveSpeed => moveSpeed;
    public float GetAttackDelay => attackDelay;
    public float GetDamage => damage;
}
