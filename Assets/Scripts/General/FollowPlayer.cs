using UnityEngine;
using UnityEngine.InputSystem;

public class FollowPlayer : MonoBehaviour
{
    [Tooltip("Move to player position when distance between this GO and player reachs this value")]
    [SerializeField] float moveDistance;
    GameObject player;

    void Start()
    {
        player = FindObjectOfType<PlayerInput>().gameObject;
    }

    void Update()
    {
        if (GetDistanceToPlayer > moveDistance) {
            transform.position = player.transform.position;
        }
    }

    float GetDistanceToPlayer => Vector2.Distance(transform.position, player.transform.position);
}
