using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractableObject))]
public class SwitchObject : MonoBehaviour
{
    [Header("Option")]
    [SerializeField] bool isOneShootInteract;
    [SerializeField] bool isSetOnWhenInteract;

    [Header("Status")]
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
            if (isSetOnWhenInteract)
            {
                isOn = !isOn;
            }

            if (isOneShootInteract)
            {
                interactableObject.StopInteract();
            }
        }
    }
}
