using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(InteractableObject))]
public class ShowObject : MonoBehaviour
{
    [SerializeField] GameObject objToShow;

    /*[Header("Debug")]
    [SerializeField] bool isShowing;*/

    InteractableObject interactableObject;

    // Start is called before the first frame update
    void Start()
    {
        interactableObject = GetComponent<InteractableObject>();
    }

    // Update is called once per frame
    void Update()
    {
        objToShow.SetActive(interactableObject.isInteracted);
    }
}
