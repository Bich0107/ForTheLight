using UnityEngine;

public class Projectile : MonoBehaviour, IHitByPlayer
{
    [SerializeField] ProjectileSO projectileScript;
    MovingObject movingObject;
    RotateObject rotateObject;
    HealthController healthController;

    void Awake()
    {
        movingObject = GetComponent<MovingObject>();
        rotateObject = GetComponent<RotateObject>();
        healthController = GetComponent<HealthController>();

        movingObject?.SetMoveSpeed(projectileScript.GetMoveSpeed);
        rotateObject?.SetRotateSpeed(projectileScript.GetRotateSpeed);
        healthController?.AddEventOnHealthReachZero((object _obj) => Die());
    }

    public void Fire(Vector2 _direction) {
        movingObject.Move(_direction);
        rotateObject.Rotate();
    }

    void OnTriggerEnter2D(Collider2D other) {
        IHitByEnemy hit = other.GetComponent<IHitByEnemy>();
        if (hit != null) {
            hit.Hit(projectileScript.GetDamage);
            Die();
        } 
    }

    public void Hit(float _damage) {
        healthController?.DecreaseHealth(_damage);
    }

    void Die() => gameObject.SetActive(false);

    void OnDisable() {
        Reset();
    }

    void Reset() {
        movingObject.Stop();
        rotateObject.Stop();
        healthController.Reset();
    }
}
