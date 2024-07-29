using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Tooltip("List of vector3 position that relative to object position")]
    [SerializeField] List<Vector3> targetList;
    [SerializeField] List<float> moveTimeList;
    [SerializeField] List<float> delayList;
    [Space]

    [SerializeField] float minDistanceToPos;
    int index = 0;
    float timer = 0;
    Vector3 startPos;
    Vector3 targetPos;
    bool isMoving;

    private void Start()
    {
        StartCoroutine(CR_Move());
    }

    void FixedUpdate()
    {
        if (!isMoving) return;
        timer += Time.fixedDeltaTime / moveTimeList[index];
        transform.position = Vector2.Lerp(startPos, targetPos, timer);
    }

    IEnumerator CR_Move()
    {
        do
        {
            startPos = transform.position;
            targetPos = targetList[index] + transform.position;
            isMoving = true;
            timer = 0;

            while (!IsMoveFinish()) yield return null;
            transform.position = targetPos;
            isMoving = false;

            yield return new WaitForSeconds(delayList[index]);

            NextIndex();
        } while (true);
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
