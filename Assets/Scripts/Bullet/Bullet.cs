using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] TrailRenderer trail;
    [SerializeField] MovingObject movingObject;
    [SerializeField] float maxDmgMultiplier = 1.8f;
    [SerializeField] Vector3 maxScale;
    [SerializeField] float maxHitCountWhenCharge = 2;
    [SerializeField] float maxHitCount = 1;
    Vector3 baseScale;
    float hitCount = 0; // the number of enemy bullet can hit before deactive
    float damage;

    void Awake()
    {
        baseScale = transform.localScale;
        trail = GetComponent<TrailRenderer>();
        movingObject = GetComponent<MovingObject>();
    }

    void OnEnable()
    {
        trail.Clear();
    }

    public void Shoot(Vector2 _direction, float _speedMultiplier, float _damage)
    {
        if (movingObject == null) return;
        ToggleChildren(); // set active children gObjects

        hitCount = maxHitCount;
        damage = _damage;
        movingObject.Move(_direction, _speedMultiplier);
    }

    public void Shoot(Vector2 _direction, float _speedMultiplier, float _damage, float _chargePercent)
    {
        if (movingObject == null) return;
        ToggleChildren(); // set active children gObjects

        hitCount = maxHitCountWhenCharge; // increase hit count
        transform.localScale = maxScale * _chargePercent / 100f; // make bullet bigger
        damage = _damage * maxDmgMultiplier * _chargePercent; // increase damage base on charge percent & max dmg multiplier
        movingObject.Move(_direction, _speedMultiplier);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        IHitByPlayer hit = other.GetComponent<IHitByPlayer>();
        if (hit != null)
        {
            if (enemy != null && enemy.IsDead) return;

            hit.Hit(damage);
            hitCount--;

            // create hit effect
            GameObject hitEffect = ObjectPool.Instance.Spawn(Tags.BulletHitVFX);
            hitEffect.transform.position = transform.position;
            hitEffect.SetActive(true);

            if (hitCount <= 0) // kill the bullet
            {
                gameObject.SetActive(false);
            }
        }
    }

    void ToggleChildren()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            child.gameObject.SetActive(!child.gameObject.activeSelf);
        }
    }

    void OnDisable()
    {
        Reset();
    }

    void Reset()
    {
        movingObject.Stop();
        transform.localScale = baseScale;
        ToggleChildren();
    }
}
