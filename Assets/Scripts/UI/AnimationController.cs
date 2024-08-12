using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] List<SimpleAnimation> animationsList;

    public void PlayAnimations() {
        for (int i = 0; i < animationsList.Count; i++) {
            animationsList[i].Play();
        }
    }

    public void RewindAnimations() {
        for (int i = 0; i < animationsList.Count; i++) {
            animationsList[i].Rewind();
        }
    }
}
