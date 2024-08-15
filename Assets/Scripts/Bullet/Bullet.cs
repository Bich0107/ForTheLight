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
    float hitCount = 0;
    float damage;

    private void Awake()
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
        ToggleChildren();

        hitCount = maxHitCount;
        damage = _damage;
        movingObject.Move(_direction, _speedMultiplier);
    }

    public void Shoot(Vector2 _direction, float _speedMultiplier, float _damage, float _chargePercent)
    {
        if (movingObject == null) return;
        ToggleChildren();

        hitCount = maxHitCountWhenCharge;
        transform.localScale = maxScale * _chargePercent;
        damage = _damage * maxDmgMultiplier * _chargePercent;
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
            if (hitCount <= 0) { 
                GameObject hitEffect = ObjectPool.Instance.Spawn(Tags.BulletHitVFX);
                hitEffect.transform.position = transform.position;
                hitEffect.SetActive(true);
                
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
