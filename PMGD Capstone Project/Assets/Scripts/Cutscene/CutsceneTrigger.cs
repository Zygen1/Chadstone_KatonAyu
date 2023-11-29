using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneTrigger : MonoBehaviour
{
    public GameObject cutscene;
    [Header("Interact")]
    public bool interactTrigger;
    public InteractableObject interactableObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Update()
    {
        if (interactTrigger)
        {
            if (interactableObject.isInteracted)
            {
                cutscene.SetActive(true);
                interactableObject.StopInteract();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           cutscene.SetActive(true);
           Destroy(gameObject);
        }
    }

    public void StartCutscene()
    {
        cutscene.SetActive(true);
        Destroy(gameObject);
    }
}
