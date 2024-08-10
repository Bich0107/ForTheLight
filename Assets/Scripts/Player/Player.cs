using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] HealthController health;
    [SerializeField] PlayerActionController actionController;
    [SerializeField] PlayerMovementController movementController;

    void Start()
    {
        health = GetComponent<HealthController>();
        actionController = GetComponent<PlayerActionController>();
        movementController = GetComponent<PlayerMovementController>();
    }

    public void Reset()
    {
        actionController?.ResetFireStatus();
        movementController?.Reset();
        health?.Reset();
    }
}
