using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SplineMesh;

public class InputEvent : MonoBehaviour
{
    public delegate void Inputdelegate();
    public static event Inputdelegate OnInput;
    [SerializeField] Spline spline;
    [SerializeField] private float time_position;
    [SerializeField] private float x_value;
    [SerializeField] private float noderange = 2f;
    [SerializeField] private float multiplyer = 1f;
    private void Start()
    {
        OnInput += GrowBranchOnTimePosition;
        spline.GenerateMain();
    }

    private void OnDisable()
    {
        OnInput -= GrowBranchOnTimePosition;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnInput();
        }
    }
    private void GrowBranchOnTimePosition()
    {
        spline.GenerateBranch(new Vector3(0, time_position, 0), new Vector3(0, time_position, 0), new Vector3(noderange * multiplyer, time_position+GenerateRandomYNumber(), 0), new Vector3(noderange * multiplyer, 0, 0));
    }

    private float GenerateRandomYNumber()
    {
        float random_number;

        random_number = Random.Range(0.1f, 10.9f);

        return random_number;
    }


}
