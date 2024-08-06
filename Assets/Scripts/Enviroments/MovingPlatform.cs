using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Tooltip("List of vector3 position that relative to object position")]
    [SerializeField] List<Vector3> targetList;
    [SerializeField] List<float> moveTimeList;
    [SerializeField] List<float> delayList;
    [SerializeField] bool loop = true;
    [SerializeField] bool playOnEnable = true;
    [Space]
    [SerializeField] float minDistanceToPos;

    int index = 0;
    float timer = 0;
    Vector3 startPos;
    Vector3 targetPos;
    bool isMoving;


    void OnEnable()
    {
        if (playOnEnable) StartCoroutine(CR_Move());
    }

    void FixedUpdate()
    {
        Moving();
    }

    void Moving()
    {
        if (!isMoving) return;

        if (moveTimeList[index] <= Mathf.Epsilon)
        {
            transform.position = targetPos;
        }
        else
        {
            if (moveTimeList[index] == 0) Debug.Log(moveTimeList[index], gameObject);
            timer += Time.fixedDeltaTime / moveTimeList[index];
            transform.position = Vector2.Lerp(startPos, targetPos, Mathf.Min(timer, 1f));
        }
    }

    IEnumerator CR_Move()
    {
        do
        {
            startPos = transform.position;
            targetPos = transform.position + targetList[index];
            isMoving = true;
            timer = 0;

            while (!IsMoveFinish()) yield return null;
            transform.position = targetPos;
            isMoving = false;

            yield return new WaitForSeconds(delayList[index]);

            NextIndex();
        } while (loop);
    }

    void NextIndex()
    {
        index = (index + 1) % targetList.Count;
    }

    bool IsMoveFinish()
    {
        return Vector3.Distance(targetPos, transform.position) <= minDistanceToPos;
    }
}
