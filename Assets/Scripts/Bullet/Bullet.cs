using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] MovingObject movingObject;
    [SerializeField] float maxDmgMultiplier = 1.8f;
    [SerializeField] Vector3 maxScale;
    Vector3 baseScale;
    float damage;

    private void Awake() {
        baseScale = transform.localScale;
    }

    private void OnEnable() {
        movingObject = GetComponent<MovingObject>();
    }

    public void Shoot(Vector2 _direction, float _speedMultiplier, float _damage) {
        if (movingObject == null) return;
        ToggleChildren();

        damage = _damage;
        movingObject.Move(_direction, _speedMultiplier);
    }

    public void Shoot(Vector2 _direction, float _speedMultiplier, float _damage, float _chargePercent) {
        if (movingObject == null) return;
        ToggleChildren();

        transform.localScale = maxScale * _chargePercent;
        damage = _damage * maxDmgMultiplier * _chargePercent;
        movingObject.Move(_direction, _speedMultiplier);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        IHitByPlayer hit = other.GetComponent<IHitByPlayer>();
        if (hit != null) {
            hit.Hit(damage);
            gameObject.SetActive(false);
        }
    }

    void ToggleChildren() {
        for (int i = 0; i < transform.childCount; i++) {
            var child = transform.GetChild(i);
            child.gameObject.SetActive(!child.gameObject.activeSelf);
        }
    }

    void OnDisable() {
        Reset();    
    }

    void Reset() {
        movingObject.Stop();
        transform.localScale = baseScale;
        ToggleChildren();
    }
}
