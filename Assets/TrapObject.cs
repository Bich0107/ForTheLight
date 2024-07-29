using UnityEngine;

public class TrapObject : MovingPlatform
{
    [SerializeField] float collideDamage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        IHitByEnemy hit = other.GetComponent<IHitByEnemy>();

        if (hit != null)
        {
            hit.Hit(collideDamage);
        }
    }
}
