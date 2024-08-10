using System;
using UnityEngine;

public class RotateCameraPlatform : MonoBehaviour
{
    [SerializeField] RotateObject camRotater;
    [SerializeField] float rotateSpeed;
    [SerializeField] float duration;
    float timer = 0;
    bool rotating = false;

    void Update() {
        if (!rotating) return;

        timer += Time.deltaTime;
        if (timer >= duration) {
            StopRotate();
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag(Tags.Player)) {
            rotating = true;
            RotateCamera();
        }
    }

    void RotateCamera() {
        camRotater.SetRotateSpeed(rotateSpeed);
        camRotater.Rotate();
    }

    void StopRotate() {
        rotating = false;
        camRotater.Stop();
    }

    void OnDisable() {
        camRotater.Reset();
    }
}
