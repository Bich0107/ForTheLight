using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(HealthController), typeof(MovementController))]
public class Enemy : MonoBehaviour, IHitByPlayer, IProtectedByShielder
{
    [SerializeField] protected GameObject player;
    protected MovementController moveController;
    protected HealthController healthController;

    protected void OnEnable()
    {
        player = FindObjectOfType<PlayerInput>().gameObject;
        moveController = GetComponent<MovementController>();
        healthController = GetComponent<HealthController>();

        healthController?.AddEventOnHealthReachZero((object _obj) => Die());
    }

    public void Hit(float _dmg) {
        healthController?.DecreaseHealth(_dmg);
    }

    protected virtual void Die() {
        gameObject.SetActive(false);
    }

    protected void OnDisable() {
        Reset();
    }

    public void Reset() {
        moveController?.Reset();
        healthController?.Reset();
    }

    public GameObject GetPlayer => player;
    protected Vector2 GetDirectionToPlayer => (player.transform.position - transform.position).normalized;
    protected float GetDistanceToPlayer => Vector2.Distance(transform.position, player.transform.position);
    protected float GetHorizontalDistanceToPlayer => Mathf.Abs(transform.position.x - player.transform.position.x);
    protected float GetVerticalDistanceToPlayer => Mathf.Abs(transform.position.y - player.transform.position.y);
}
