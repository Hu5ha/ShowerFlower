using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputTimer : MonoBehaviour
{

    [SerializeField] private float start_timer = 0f;
    [SerializeField] private float timer = float.NaN;
    [SerializeField] private float max_timer;

    //INputEventScript
    InputEvent inputEvent;

    private void Start()
    {
        InputEvent.OnInput += StartTimer;
        InputEvent.OnInputWithParameter += StartTimerParameter;
        inputEvent = GetComponent<InputEvent>();
    }

    private void StartTimer()
    {
        timer = start_timer;
        InputEvent.OnInput -= StartTimer;
        InputEvent.OnInputWithParameter -= StartTimerParameter;
    }

    private void StartTimerParameter(float boost = 0)
    {
        StartTimer();
    }

    private void CountTimerUp()
    {
        timer += Time.deltaTime;
    }

    public float GetCurrentTimerValue()
    {
        return Mathf.Round(timer * 10) * 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer != float.NaN && timer < max_timer)
        {
            CountTimerUp();
        }
    }


}
