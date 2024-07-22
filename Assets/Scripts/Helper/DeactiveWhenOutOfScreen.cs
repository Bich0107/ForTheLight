using System.Collections;
using System.Collections.Generic;
using ExtensionMethods;
using UnityEngine;

public class DeactiveWhenOutOfScreen : MonoBehaviour
{
    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        CheckPosition();
    }

    void CheckPosition() {
        if (cam.IsOutOfScreen(transform.position)) {
            gameObject.SetActive(false);
        }
    }
}
