using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SplineMesh;

public class InputEvent : MonoBehaviour
{
    //events
    public delegate void Inputdelegate();
    public static event Inputdelegate OnInput;
    public static event InputWithParameter KeyPressLeft;
    public static event InputWithParameter KeyPressRight;

    public delegate void InputWithParameter(float boost);
    public static event InputWithParameter OnInputWithParameter;
    public static event InputWithParameter NewBranchPosition;

    //spline prefab
    [SerializeField] List<Spline> spline = new List<Spline>();
    [SerializeField] GameObject spline_prefab;
    [SerializeField] int spline_counter = 0;
    [SerializeField] float scale_value;
    [SerializeField] private float branch_animation_duration = 1;
    [SerializeField] private float spline_start_scale = 2;
    [SerializeField] private Vector3 spline_scale = new Vector3(1, 1, 1);

    [SerializeField] private Vector3 next_spline_end;
    [SerializeField] private Vector3 next_slpine_start;
    [SerializeField] private Vector3 current_spline_start;
    [SerializeField] private Vector3 current_spline_end;

    //timer
    public InputTimer time_position;

    //variables
    [SerializeField] private float height = 12f;
    [SerializeField] private int growth_direction = 1;
    //[SerializeField] private float time_position;
    [SerializeField] private float branch_end_node_range = 2f;
    [SerializeField] private float random_range = 2f;
    [SerializeField] private float noderange = 2f;
    [SerializeField] private float multiplyer = 1f;
    private void Start()
    {
        //    OnInput += GrowBranchOnTimePosition;
        OnInputWithParameter += GrowBranchOnTimePositionParameter;
        KeyPressLeft += LeftDirection;
        KeyPressRight += GrowBranchOnTimePositionParameter;
        time_position = GetComponent<InputTimer>();

        spline.Add(Instantiate(spline_prefab, new Vector3(0, 0, 0), Quaternion.identity).gameObject.transform.GetChild(0).gameObject.GetComponent<Spline>());
        spline[spline_counter].GenerateMain(height);
        //scale_value = spline[spline_counter].gameObject.GetComponent<ExampleGrowingRoot>().startScale / spline_counter;
        spline[spline_counter].gameObject.GetComponent<ExampleGrowingRoot>().startScale = spline_start_scale;
        spline[spline_counter].gameObject.GetComponent<ExampleGrowingRoot>().scale = spline_scale;
        spline[spline_counter].gameObject.GetComponent<ExampleGrowingRoot>().DurationInSecond = branch_animation_duration;


    }

    public void LeftDirection(float somethinig)
    {
        growth_direction = -1;
        OnInputWithParameter(somethinig);
    }
    public void RightDirection(float somethinig)
    {
        growth_direction = 1;
        OnInputWithParameter(somethinig);
    }

    private void OnDisable()
    {
        //OnInput -= GrowBranchOnTimePosition;
        OnInputWithParameter -= GrowBranchOnTimePositionParameter;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnInput();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            growth_direction = 1;
            OnInputWithParameter(multiplyer);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            growth_direction = -1;
            OnInputWithParameter(multiplyer);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            GrowBranchOnBranch();
        }
    }

    public float GetTreeHeight()
    {
        return height;
    }
    //private void GrowBranchOnTimePosition()
    //{
    //    spline.Add(Instantiate(spline_prefab, new Vector3(0, time_position.GetCurrentTimerValue(), 0), Quaternion.identity).gameObject.transform.GetChild(0).gameObject.GetComponent<Spline>());
    //    spline_counter++;
    //    current_spline_start = new Vector3(0, time_position.GetCurrentTimerValue(), 0);
    //    current_spline_end = new Vector3(noderange * multiplyer, time_position.GetCurrentTimerValue() + GenerateRandomYNumber(), 0);
    //    spline[spline_counter].GenerateBranch(current_spline_start, new Vector3(0, time_position.GetCurrentTimerValue(), 0), current_spline_end, new Vector3(noderange * multiplyer, time_position.GetCurrentTimerValue() + GenerateRandomYNumber(), 0));
    //    if (spline[spline_counter].gameObject.GetComponent<ExampleGrowingRoot>().startScale > .6)
    //        spline[spline_counter].gameObject.GetComponent<ExampleGrowingRoot>().startScale = spline_start_scale / (spline_counter / 2);
    //}
    //if left player: negate growth_direction
    private void GrowBranchOnTimePositionParameter(float boost)
    {
        spline.Add(Instantiate(spline_prefab, new Vector3(0, time_position.GetCurrentTimerValue(), 0), Quaternion.identity).gameObject.transform.GetChild(0).gameObject.GetComponent<Spline>());
        spline_counter++;
        current_spline_start = new Vector3(0, time_position.GetCurrentTimerValue(), 0);
        current_spline_end = new Vector3((noderange * boost) * growth_direction, time_position.GetCurrentTimerValue() + GenerateRandomYNumber(), 0);
        spline[spline_counter].GenerateBranch(current_spline_start, new Vector3((current_spline_start.x + 1) * growth_direction, time_position.GetCurrentTimerValue(), 0), current_spline_end, new Vector3((noderange * boost) * growth_direction, time_position.GetCurrentTimerValue() + GenerateRandomYNumber(), 0));
        if (spline[spline_counter].gameObject.GetComponent<ExampleGrowingRoot>().startScale > 1f)
            spline[spline_counter].gameObject.GetComponent<ExampleGrowingRoot>().startScale = spline_start_scale / (spline_counter / 2);
        NewBranchPosition(0);
    }

    private void GrowBranchOnBranch()
    {
        spline.Add(Instantiate(spline_prefab, spline[spline_counter].gameObject.transform.position, Quaternion.identity).gameObject.transform.GetChild(0).gameObject.GetComponent<Spline>());
        spline_counter++;
        spline[spline_counter].GenerateBranch(new Vector3(current_spline_end.x - 7, current_spline_end.y, 0), new Vector3(current_spline_end.x + 1, current_spline_end.y, 0), current_spline_end + new Vector3(1, 1, 0), new Vector3(current_spline_end.x + 1, current_spline_end.y, 0));
        spline[spline_counter].gameObject.GetComponent<ExampleGrowingRoot>().startScale = spline_start_scale / 10;
    }
    public float GetBranchEndPosition()
    {
        return time_position.GetCurrentTimerValue() * 2;

    }


    private float GenerateRandomYNumber()
    {
        float random_number;

        random_number = Random.Range(-random_range, random_range);

        return random_number;
    }


}
