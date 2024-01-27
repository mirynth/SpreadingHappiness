using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{
    public static Action OnMinuteChanged;

    public static int Minute { get; private set; }

    //60 means ingame time is the same as irl time
    //lower number means ingame time is faster
    private float minuteToRealTime = 60f;
    private float timer;

    void Start()
    {
        Minute = 0;
        timer = minuteToRealTime;
        
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Minute++;
            OnMinuteChanged?.Invoke();
            if(Minute >= 10)
            {
                Debug.Log("10min passed");
            }

            timer = minuteToRealTime;
        }
    }
}
