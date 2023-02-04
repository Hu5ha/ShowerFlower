using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SplineMesh;

public class InputEvent : MonoBehaviour
{
    public delegate void Inputdelegate();
    public static event Inputdelegate OnInput;
    [SerializeField] Spline spline;
    [SerializeField] 

    private void Start()
    {
        OnInput += GrowRoot;
        spline.GenerateMain();
    }

    private void OnDisable()
    {
        OnInput -= GrowRoot;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnInput();
        }
    }

    private void GrowRoot()
    {
        //TODO make spline with direction and node
        spline.GenerateBranch();
    }

    public void Timer()
    {

    }
}
