using UnityEngine;

public class ParentPlatform : MonoBehaviour
{
    bool isQuiting;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(Tags.Player))
        {
            other.transform.parent = transform;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (isQuiting) return;

        if (other.gameObject.CompareTag(Tags.Player))
        {
            other.transform.parent = null;
        }
    }

#if UNITY_EDITOR
    // to make oncollistionexit stop when exit play mode in editor
    void OnApplicationQuit()
    {
        isQuiting = true;
    }
#endif
}
