using UnityEngine;

public class RotateObject : MonoBehaviour
{
    Quaternion baseRotation;
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
    bool isQuiting;

    void Awake()
    {
        baseRotation = transform.rotation;
    }

    void OnEnable()
    {
        isQuiting = false;
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

    void OnApplicationQuit()
    {
        isQuiting = true;
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

    public void Reset()
    {
        if (isQuiting) return;

        Stop();
        transform.rotation = baseRotation;
    }
}
