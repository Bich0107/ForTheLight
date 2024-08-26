using UnityEngine;

public class CircleLaser : MonoBehaviour
{
    float damage;

    public void Initilize(float _damage) {
        damage = _damage;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        IHitByEnemy hit = other.GetComponent<IHitByEnemy>();
        if (hit != null)
        {
            hit.Hit(damage);
        }
    }
}
