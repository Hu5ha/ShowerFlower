using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputEvent : MonoBehaviour
{
    public delegate void Inputdelegate();
    public static event Inputdelegate OnInput;

    private void Start()
    {
        OnInput += GrowRoot;
    }

    private void OnDisable()
    {
        OnInput -= GrowRoot;
    }

    private void GrowRoot()
    {
        //TODO make spline with direction and node
    }
}
