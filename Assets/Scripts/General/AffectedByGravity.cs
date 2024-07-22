using UnityEngine;

public class AffectedByGravity : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundCheck;
    [Space]
    [SerializeField] Vector2 capsuleColliderChecksize;
    [SerializeField] float fallMultiplier;
    Vector2 vectorGravity;

    private void Start()
    {
        vectorGravity = new Vector2(0, -Physics2D.gravity.y);
        rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (rigid.velocity.y < 0)
        {
            rigid.velocity -= vectorGravity * fallMultiplier * Time.fixedDeltaTime;
        }
    }

    public bool CheckGround()
    {
        return Physics2D.OverlapCapsule(groundCheck.position, capsuleColliderChecksize, CapsuleDirection2D.Horizontal, 0, groundLayer);
    }
}
