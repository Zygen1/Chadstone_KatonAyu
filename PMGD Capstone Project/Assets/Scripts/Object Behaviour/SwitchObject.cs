using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractableObject))]
public class SwitchObject : MonoBehaviour
{
    public bool isOn;

    InteractableObject interactableObject;

    // Start is called before the first frame update
    void Start()
    {
        interactableObject = GetComponent<InteractableObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (interactableObject.isInteracted)
        {
            isOn = !isOn;
            interactableObject.StopInteract();
        }
    }
}
