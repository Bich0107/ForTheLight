using UnityEngine;

public class ResetTransformOnDisable : MonoBehaviour
{
    [SerializeField] Vector3 position;
    [SerializeField] Quaternion rotation;
    [SerializeField] Vector3 scale;
    [SerializeField] bool isLocalTransform = false;

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
