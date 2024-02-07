using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public static Action OnMinuteChanged;

    public TMP_Text text;
    public static int Minute { get; private set; }
    
    //60 means ingame time is the same as irl time
    //lower number means ingame time is faster
    private float minuteToRealTime = 60f;
    private float seconds;

    void Start()
    {
        Minute = 0;
        seconds = 0;
        text.text = "0:00";
    }

    void Update()
    {
        seconds += Time.deltaTime;

        if (seconds >= 59)
        {
            Minute++;
            OnMinuteChanged?.Invoke();
            if(Minute >= 10)
            {
                Debug.Log("10min passed");
            }

            seconds -= minuteToRealTime;
        }
        //text.SetText($"{0:00} : {1:00}", Minute, seconds);
        text.text = Minute.ToString() + ":" + seconds.ToString("00");
    }
}
