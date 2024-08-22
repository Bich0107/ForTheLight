using UnityEngine;

public class ResetTransformOnDisable : MonoBehaviour
{
    [SerializeField] bool isLocalTransform = false;
    Vector3 position;
    Quaternion rotation;
    Vector3 scale;

    void Awake()
    {
        if (isLocalTransform)
        {
            position = transform.localPosition;
            rotation = transform.localRotation;
        }
        else
        {
            position = transform.position;
            rotation = transform.rotation;
        }

        scale = transform.localScale;
    }

    void OnDisable()
    {
        if (isLocalTransform)
        {
            transform.localPosition = position;
            transform.localRotation = rotation;
        }
        else
        {
            transform.position = position;
            transform.rotation = rotation;
        }

        transform.localScale = scale;
    }
}
