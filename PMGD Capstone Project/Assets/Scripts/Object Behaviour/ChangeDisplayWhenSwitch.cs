using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SwitchObject))]
public class ChangeDisplayWhenSwitch : MonoBehaviour
{
    [SerializeField] GameObject display1;
    [SerializeField] GameObject display2;

    SwitchObject switchObject;

    // Start is called before the first frame update
    void Start()
    {
        switchObject = GetComponent<SwitchObject>();
    }

    // Update is called once per frame
    void Update()
    {
        display1.SetActive(!switchObject.isOn);
        display2.SetActive(switchObject.isOn);
    }
}
