using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour, IHitByPlayer
{
    [SerializeField] protected ProjectileSO projectileScript;
    protected GameObject target;
    protected MovingObject movingObject;
    protected HealthController healthController;
    protected Coroutine attackCoroutine;

    void Awake()
    {
        movingObject = GetComponent<MovingObject>();
        healthController = GetComponent<HealthController>();

        movingObject?.SetMoveSpeed(projectileScript.GetMoveSpeed);
        healthController?.AddEventOnHealthReachZero((object _obj) => Die());
    }

    public virtual void Fire(GameObject _target) {}

    public virtual void Fire(Vector2 _direction) {
        Rotate(_direction);
        attackCoroutine = StartCoroutine(CR_Fire(_direction));
    }

    protected void Rotate(Vector2 _direction) {
        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;
    }

    protected virtual IEnumerator CR_Fire(Vector2 _direction) {
        yield return new WaitForSeconds(projectileScript.GetAttackDelay);

        movingObject.Move(_direction);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other) {
        IHitByEnemy hit = other.GetComponent<IHitByEnemy>();
        if (hit != null) {
            hit.Hit(projectileScript.GetDamage);
            Die();
        } 
    }

    public virtual void Hit(float _damage) {
        healthController?.DecreaseHealth(_damage);
    }

    protected void Die() => gameObject.SetActive(false);

    void OnDisable() {
        Reset();
    }

    protected virtual void Reset() {
        if (attackCoroutine != null) StopCoroutine(attackCoroutine);
        movingObject?.Stop();
        healthController?.Reset();
    }
}
