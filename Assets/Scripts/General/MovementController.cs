using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementController : MonoBehaviour
{
    Rigidbody2D rigid;
    Vector2 direction;
    Vector2 velocity = Vector2.zero;
    [SerializeField] float baseSpeed;
    [SerializeField] float moveSpeed;
    public float MoveSpeed {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }

    [SerializeField] bool onlyHorizontal = false;
    [SerializeField] bool onlyVertical = false;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        Reset();
    }

    private void FixedUpdate()
    {
        if (onlyVertical) velocity.x = rigid.velocity.x;
        else velocity.x = direction.x * moveSpeed;

        if (onlyHorizontal) velocity.y = rigid.velocity.y;
        else velocity.y = direction.y * moveSpeed;

        rigid.velocity = velocity;
    }

    public void ModifiyMoveSpeed(float _modifier) {
        moveSpeed = baseSpeed * _modifier;
    }

    public void Move(Vector2 _direction)
    {
        direction = _direction;
    }

    public void Stop()
    {
        direction = Vector2.zero;
        rigid.velocity = Vector2.zero;
    }

    public void Reset()
    {
        Stop();
        moveSpeed = baseSpeed;
    }

    public void BlockHorizontal() => onlyVertical = true;
    public void BlockVertical() => onlyHorizontal = true;
}
