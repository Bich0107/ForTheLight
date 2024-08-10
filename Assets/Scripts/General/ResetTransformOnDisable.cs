using UnityEngine;

public class ResetTransformOnDisable : MonoBehaviour
{
    Vector3 position;
    Quaternion rotation;
    Vector3 scale;

    void Awake()
    {
        position = transform.position;
        rotation = transform.rotation;
        scale = transform.localScale;
    }

    void OnDisable()
    {
        transform.position = position;
        transform.rotation = rotation;
        transform.localScale = scale;
    }
}
