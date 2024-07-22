using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] MovementController moveController;
    [SerializeField] JumpController jumpController;

    void Start()
    {
        moveController = GetComponent<MovementController>();
        jumpController = GetComponent<JumpController>();
    }

    void OnMove(InputValue _value)
    {
        moveController.Move(_value.Get<Vector2>());
    }
}
