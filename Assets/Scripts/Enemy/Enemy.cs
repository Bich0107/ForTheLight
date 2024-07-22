using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(HealthController), typeof(MovementController))]
public class Enemy : MonoBehaviour, IHitByPlayer
{
    [SerializeField] protected GameObject player;
    protected MovementController moveController;
    protected EnemyAttackController attackController;
    protected HealthController healthController;

    protected void OnEnable()
    {
        player = FindObjectOfType<PlayerInput>().gameObject;
        moveController = GetComponent<MovementController>();
        attackController = GetComponent<EnemyAttackController>();
        healthController = GetComponent<HealthController>();

        healthController?.AddEventOnHealthReachZero((object _obj) => Die());
    }

    public void Hit(float _dmg) {
        healthController?.DecreaseHealth(_dmg);
    }

    protected void Die() {
        gameObject.SetActive(false);
    }

    protected void OnDisable() {
        Reset();
    }

    public void Reset() {
        moveController?.Reset();
        attackController?.Reset();
        healthController?.Reset();
    }

    public GameObject GetPlayer => player;
    protected Vector2 GetDirectionToPlayer => (player.transform.position - transform.position).normalized;
    protected float GetDistanceToPlayer => Vector2.Distance(transform.position, player.transform.position);
}
