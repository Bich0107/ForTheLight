using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] HealthController health;
    [SerializeField] PlayerActionController actionController;
    [SerializeField] PlayerMovementController movementController;
    [SerializeField] PlayerCollisionHandler collisionHandler;

    void Start()
    {
        health = GetComponent<HealthController>();
        actionController = GetComponent<PlayerActionController>();
        movementController = GetComponent<PlayerMovementController>();
        collisionHandler = GetComponent<PlayerCollisionHandler>();
    }

    public void ToggleCollision() {
        collisionHandler.ToggleActive();
    }

    public void Reset()
    {
        actionController?.ResetFireStatus();
        movementController?.Reset();
        health?.Reset();
    }
}
