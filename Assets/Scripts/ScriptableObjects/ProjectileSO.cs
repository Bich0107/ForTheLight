using UnityEngine;

[CreateAssetMenu(menuName = "Projectile", fileName = "New projectile")]
public class ProjectileSO : ScriptableObject
{
    [SerializeField] new string name;
    [SerializeField] float moveSpeed;
    [SerializeField] float rotateSpeed;
    [SerializeField] float damage;

    public string GetName => name;
    public float GetMoveSpeed => moveSpeed;
    public float GetRotateSpeed => rotateSpeed;
    public float GetDamage => damage;
}
