using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RotateCameraPlatform : MonoBehaviour
{
    [SerializeField] GameObject cam;
    [SerializeField] Transform rotatingPoint;
    [SerializeField] float angle;
    Vector2 baseAngle;

    void Start()
    {
    }

    private void Update() {
        RotateCamera();
    }

    // void OnCollisionEnter2D(Collision2D other)
    // {
    //     if (other.gameObject.CompareTag(Tags.Player))
    //     {
    //         RotateGravity();
    //     }
    // }

    // void OnCollisionStay2D(Collision2D other)
    // {
    //     if (other.gameObject.CompareTag(Tags.Player))
    //     {
    //         RotateGravity();
    //     }
    // }

    // void OnCollisionExit2D(Collision2D other)
    // {
    //     RestoreGravity();
    // }

    // rotate gravity vector along object rotation
    void RotateCamera() {

    }
}
