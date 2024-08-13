using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeKeeper : MonoBehaviour
{
    [SerializeField] float timer;
    public float Timer => timer;
    bool isCounting = false;

    void Update()
    {
        if (isCounting)
        {
            timer += Time.deltaTime;
        }
    }

    public void StartTimer() => isCounting = true;

    public void StopTimer() => isCounting = false;

    public void ResetTimer() => timer = 0;
}
