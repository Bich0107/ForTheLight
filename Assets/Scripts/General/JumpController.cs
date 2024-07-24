using UnityEngine;
using UnityEngine.InputSystem;

public class JumpController : MonoBehaviour
{
    Rigidbody2D rigid;
    Vector2 velocity = Vector2.zero;
    [SerializeField] AffectedByGravity affectedByGravity;
    [SerializeField] int airJumpCount = 1;
    int counter;
    [Space]
    [SerializeField] float jumpForce;


    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        affectedByGravity = GetComponent<AffectedByGravity>();
    }

    void Jump()
    {
        if (!affectedByGravity.CheckGround())
        {
            if (counter < airJumpCount)
            {
                counter++;

                velocity.x = rigid.velocity.x;
                velocity.y = jumpForce;
                rigid.velocity = velocity;
            }
            return;
        }

        counter = 0;
        velocity.x = rigid.velocity.x;
        velocity.y = jumpForce;
        rigid.velocity = velocity;
    }

    void OnJump(InputValue _value)
    {
        if (_value.isPressed)
        {
            Jump();
        }
    }
}
