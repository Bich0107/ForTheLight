using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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
         //   GameManager.Instance.SetPlayerControlStatus(false);
            rotating = true;
            other.transform.parent = transform;
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
