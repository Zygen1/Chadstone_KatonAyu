using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchState : MonoBehaviour
{
    public GameObject switchUp;
    public GameObject lightsOn;
    public bool isUp;
    public bool isOn;

    // Start is called before the first frame update
    void Start()
    {
        switchUp.SetActive(isUp);
        lightsOn.SetActive(isOn);

        if (isOn)
        {
            ElectricityPuzzle.Instance.SwitchChange(1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        isUp = !isUp;
        isOn = !isOn;

        switchUp.SetActive(isUp);
        lightsOn.SetActive(isOn);

        if (isOn)
        {
            ElectricityPuzzle.Instance.SwitchChange(1);
        }
        else
        {
            ElectricityPuzzle.Instance.SwitchChange(-1);
        }
    }
}
