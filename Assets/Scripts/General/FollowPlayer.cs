using UnityEngine;
using UnityEngine.InputSystem;

public class FollowPlayer : MonoBehaviour
{
    [Tooltip("Move to player position whenever distance between this GO and player exceed this value")]
    [SerializeField] float moveDistance;
    [SerializeField] Vector3 offset = new Vector3();
    GameObject player;

    void Start()
    {
        player = FindObjectOfType<PlayerInput>().gameObject;
    }

    void LateUpdate()
    {
        if (GetDistanceToPlayer > moveDistance) {
            transform.position = player.transform.position + offset;
        }
    }

    float GetDistanceToPlayer => Vector2.Distance(transform.position, player.transform.position);
}
