using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputTimer : MonoBehaviour
{

    [SerializeField] private float start_timer = 0f;
    [SerializeField] private float timer = -1f;
    [SerializeField] private float max_timer;

    //INputEventScript
    InputEvent inputEvent;

    private void Start()
    {
        InputEvent.OnInput += StartTimer;
        inputEvent = GetComponent<InputEvent>();
        max_timer = inputEvent.GetTreeHeight();
    }

    private void StartTimer()
    {
        timer = start_timer;
        InputEvent.OnInput -= StartTimer;
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
        if(timer > -1f && timer < max_timer)
        {
            CountTimerUp();
        }
    }


}
