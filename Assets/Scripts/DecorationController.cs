using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationController : MonoBehaviour
{

    [SerializeField] private GameObject flower;
    [SerializeField] private Vector3 position_origin;
    private InputEvent inputEvent;

    // Start is called before the first frame update
    void Start()
    {
        inputEvent = GetComponent<InputEvent>();
        InputEvent.NewBranchPosition += Decorate;
    }

    private void Decorate(float position)
    {
        
       Instantiate(flower, new Vector3(0,inputEvent.GetBranchEndPosition(),-2), Quaternion.Euler(-90, 0, 0));
        Instantiate(flower, GenerateRandomVector(), Quaternion.Euler(-90, 0, 0));

    }

    private Vector3 GenerateRandomVector()
    {
        float y_value = inputEvent.time_position.GetCurrentTimerValue();
        float x_value = Random.Range(-10, 10);
        return new Vector3(x_value, y_value, -3);
    }


    // Update is called once per frame
    void Update()
    {

    }
}
