using System;
using UnityEngine;

public class RotateCameraPlatform : MonoBehaviour
{
    [SerializeField] RotateObject camRotater;
    [SerializeField] float rotateSpeed;
    [SerializeField] float duration;
    Action<object> onFinishRotating;
    float timer = 0;
    bool rotating = false;

    void Update() {
        if (!rotating) return;

        timer += Time.deltaTime;
        if (timer >= duration) {
            onFinishRotating.Invoke(null);
            StopRotate();
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag(Tags.Player)) {
            rotating = true;
            RotateCamera();
        }
    }

    public void AddFinishRotateEvent(Action<object> _action) {
        onFinishRotating += _action;
    }

    void RotateCamera() {
        camRotater.SetRotateSpeed(rotateSpeed);
        camRotater.Rotate();
    }

    void StopRotate() {
        rotating = false;
        camRotater.Stop();
    }
}
