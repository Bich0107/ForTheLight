using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingBlade : MonoBehaviour
{
    [SerializeField] RotateObject rotateObject;

    [SerializeField] List<float> speedList;
    [Tooltip("Time for the object's rotation speed to reach he target speed")]
    [SerializeField] List<float> accelTimeList;
    [SerializeField] List<float> delayList;
    [Space]
    [SerializeField] float minError;

    int index = 0;
    float timer = 0;
    float startSpeed;
    float targetSpeed;
    bool isChangingSpeed;

    void Start()
    {
        rotateObject = GetComponent<RotateObject>();

        StartCoroutine(CR_Rotate());
    }

    void FixedUpdate() {
        if (!isChangingSpeed) return;

        timer += Time.fixedDeltaTime / accelTimeList[index];
        rotateObject.RotateSpeed = Mathf.Lerp(startSpeed, targetSpeed, timer);
    }

    IEnumerator CR_Rotate()
    {
        do
        {
            startSpeed = rotateObject.RotateSpeed;
            targetSpeed = speedList[index];
            isChangingSpeed = true;
            timer = 0;

            while (!IsFinishAccelerate()) yield return null;
            rotateObject.RotateSpeed = targetSpeed;

            isChangingSpeed = false;

            yield return new WaitForSeconds(delayList[index]);

            NextIndex();
        } while (true);
    }

    void NextIndex()
    {
        index = (index + 1) % speedList.Count;
    }

    bool IsFinishAccelerate()
    {
        return Mathf.Abs(rotateObject.RotateSpeed - targetSpeed) <= minError;
    }
}
