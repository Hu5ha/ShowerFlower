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
    }

    // Update is called once per frame
    void Update()
    {

    }
}
