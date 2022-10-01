using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{

    private int currentTime = 10;
    public float waitBetweenCountDown = 3;
    public UnityEvent OnCountDown;

    public static Timer Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountDown());
    }

    private IEnumerator CountDown()
    {
        yield return new WaitForSeconds(1);
        currentTime--;
        if (currentTime == 0)
        {
            OnCountDown.Invoke();
            yield return new WaitForSeconds(waitBetweenCountDown);
            currentTime = 10;
        }

        //TODO: Check if player is not dead yet, if dead stop timer
        StartCoroutine(CountDown());
    }

    public int GetCurrentTime() => currentTime;
}
