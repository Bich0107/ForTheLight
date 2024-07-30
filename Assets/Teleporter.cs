using System.Collections;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] Transform destination;
    [SerializeField] float delayTime;
    bool isTeleporting;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Tags.Player))
        {
            Teleport(other.gameObject);
        }
    }

    void Teleport(GameObject _target)
    {
        if (isTeleporting) return;
        isTeleporting = true;
        StartCoroutine(CR_Teleport(_target));
    }

    IEnumerator CR_Teleport(GameObject _target)
    {
        GameManager.Instance.SetPlayerControlStatus(false);
        yield return new WaitForSeconds(delayTime);

        _target.transform.position = destination.position;
        GameManager.Instance.SetPlayerControlStatus(true);
    }
}
