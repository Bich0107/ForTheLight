using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RotateObject : MonoBehaviour
{
    [SerializeField] float rotateSpeed;
    [SerializeField] float duration; // negative duration for infinite duration
    bool isRotating;
    float timer;

    void FixedUpdate()
    {
        if (isRotating)
        {
            timer += Time.fixedDeltaTime;
            if (timer > duration && duration > 0) Stop();
            transform.Rotate(Vector3.forward * rotateSpeed * Time.fixedDeltaTime);
        }
    }

    public void SetRotateSpeed(float _rotateSpeed) => rotateSpeed = _rotateSpeed;

    public void Rotate(float _duration = -1f)
    {
        isRotating = true;
        timer = 0;
        duration = _duration;
    }

    public void Stop()
    {
        isRotating = false;
        timer = 0;
    }
}
