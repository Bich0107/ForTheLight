using UnityEngine;

public class DangerouseObject : MonoBehaviour
{
    [SerializeField] float collideDamage;

    public void Initialize(float _damage) {
        collideDamage = _damage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IHitByEnemy hit = other.GetComponent<IHitByEnemy>();

        if (hit != null)
        {
            hit.Hit(collideDamage);
        }
    }
}
