using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    private int hour;
    private int minute;
    private int second;

    private bool isclockactive;

    private void InitialiseClock()
    {
        hour = 0;
        minute = 0;
        second = 0;
    }

    public void StartClock()
    {
        InitialiseClock();
        isclockactive = true;

        StartCoroutine(UpdateTime());
    }
    public void StopClock()
    {
        isclockactive = false;
        StopCoroutine(UpdateTime());
    }

    IEnumerator UpdateTime()
    {
        while(isclockactive)
        {
            yield return new WaitForSeconds(1);

            second++;

            if (second >= 60)
            {
                second = 0;
                minute++;
            }
            if (minute >= 60)
            {
                minute = 0;
                hour++;
            }
        }
    }

    public string DisplayTime()
    {
        return minute + ":" + second;
    }
}