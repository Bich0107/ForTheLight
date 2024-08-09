using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TrigerrScriptDeleteMe : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("trigerred");
    }

    void OnParticleTrigger() {
        Debug.Log("trigerred 2");
        
    }
}
