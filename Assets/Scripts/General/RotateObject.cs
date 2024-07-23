using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RotateObject : MonoBehaviour
{
    [SerializeField] float rotateSpeed;
    public float RotateSpeed
    {
        get { return rotateSpeed; }
        set { rotateSpeed = value; }
    }
    [SerializeField] float duration;
    [SerializeField] bool loop;
    [SerializeField] bool rotateOnAwake = false;
    bool isRotating;
    float timer;

    private void OnEnable() {
        if (rotateOnAwake) Rotate();
    }

    void FixedUpdate()
    {
        if (isRotating)
        {
            if (!loop)
            {
                timer += Time.fixedDeltaTime;
                if (timer > duration) Stop();
            }
            transform.Rotate(Vector3.forward * rotateSpeed * Time.fixedDeltaTime);
        }
    }

    public void SetRotateSpeed(float _value) => rotateSpeed = _value;

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
