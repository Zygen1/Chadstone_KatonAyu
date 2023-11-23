using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwitchState : MonoBehaviour
{
    public GameObject switchUp;
    public GameObject switchDown;
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

        if (isUp == true)
        {
            switchUp.SetActive(true);
            switchDown.SetActive(false);
        }
        else
        {
            switchUp.SetActive(false);
            switchDown.SetActive(true);
        }

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
