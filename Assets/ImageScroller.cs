using System.Collections;
using UnityEngine;

public class ImageScroller : MonoBehaviour
{
    [SerializeField] Material material;
    [SerializeField] Vector2 scrollSpeed;

    void Start()
    {
        material.mainTextureOffset = Vector2.zero;
        StartCoroutine(CR_Scroll());
    }

    IEnumerator CR_Scroll() {
        Vector2 scaleValue = material.mainTextureOffset;
        do {
            scaleValue += scrollSpeed * Time.unscaledDeltaTime;
            material.mainTextureOffset = scaleValue;

            yield return null;
        }
        while (true);
    }
}
