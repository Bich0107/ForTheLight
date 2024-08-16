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

    public void ToggleCollision()
    {
        collisionHandler.ToggleActive();
    }

    public void Reset()
    {
        actionController?.ResetChargeStatus();
        movementController?.Reset();
        health?.Reset();
        transform.parent = null;
    }

    void OnApplicationQuit()
    {
        transform.parent = null;
    }
}
