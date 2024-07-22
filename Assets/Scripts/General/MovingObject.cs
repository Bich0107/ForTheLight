using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovingObject : MonoBehaviour
{
    Rigidbody2D rigid;
    Vector2 direction;
    [SerializeField] float moveSpeed;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void SetMoveSpeed(float _moveSpeed) => moveSpeed = _moveSpeed;

    public void Move(Vector2 _direction, float _speedMultiplier = 1f)
    {
        direction = _direction;
        rigid.velocity = _direction * moveSpeed * _speedMultiplier;
    }

    public void Reflect(Vector2 _norm, float _speedMultiplier = 1f) {
        direction = Vector2.Reflect(direction, _norm);
        rigid.velocity = direction * moveSpeed * _speedMultiplier;
    }

    public void Stop() {
        direction = Vector2.zero;
        rigid.velocity = Vector2.zero;
    }
}
