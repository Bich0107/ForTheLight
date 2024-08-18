using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDisplayer : MonoBehaviour
{
    [SerializeField] AnimationController UIAnimation;

    void Start() {
        if (UIAnimation == null) return;
        
        UIAnimation.PlayAnimations();
    }
}
