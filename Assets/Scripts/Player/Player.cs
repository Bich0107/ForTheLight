using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] HealthDisplayer healthDisplayer;
    [SerializeField] HealthController health;
    [SerializeField] PlayerActionController actionController;
    [SerializeField] PlayerMovementController movementController;
    [SerializeField] PlayerCollisionHandler collisionHandler;

    void Start()
    {
        healthDisplayer = FindObjectOfType<HealthDisplayer>();
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
        healthDisplayer?.Reset();
        transform.parent = null;
    }

    void OnApplicationQuit()
    {
        transform.parent = null;
    }
}
