using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    public Text timerText; // 用于显示时间的文本

    void Update()
    {
        int remainingTime = Mathf.CeilToInt(Timer.GetTimeRemaining()); // 向上取整
        timerText.text = "" + remainingTime; // 只显示整数
    }
}
