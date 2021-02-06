using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerCount : MonoBehaviour
{
    public Text timerText;
    private float secondsCount;
    private int minuteCount;
    private int hourCount;
    private static float oldSecondsCount;
    private static int oldMinuteCount;
    private static int oldHourCount;


    // Start is called before the first frame update
    void Start()
    {
        secondsCount = oldSecondsCount;
        minuteCount = oldMinuteCount;
        hourCount = oldMinuteCount;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimerUI();
    }

    public void UpdateTimerUI()
    {
        //set timer UI
        secondsCount += Time.deltaTime;
        oldSecondsCount = secondsCount;
        timerText.text = hourCount + "h:" + minuteCount + "m:" + (int)secondsCount + "s";
        if (secondsCount >= 60)
        {
            minuteCount++;
            oldMinuteCount = minuteCount;
            secondsCount = 0;
        }
        else if (minuteCount >= 60)
        {
            hourCount++;
            oldHourCount = hourCount;
            minuteCount = 0;
        }
    }
}
