using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerDisplayer : MonoBehaviour
{
    [SerializeField] TimeKeeper timeKeeper;
    [SerializeField] TextMeshProUGUI timerText;

    void LateUpdate()
    {
        if (timeKeeper == null || timerText == null) return;
        
        timerText.text = StringHelper.SecondToString(timeKeeper.Timer);
    }
}
