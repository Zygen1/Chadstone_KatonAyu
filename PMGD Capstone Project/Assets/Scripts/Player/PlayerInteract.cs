using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public static PlayerInteract instance;

    [SerializeField] GameObject interactBTN;
    [SerializeField] float interactRange = 2.0f;
    [SerializeField] GameObject[] interactableObjects;
    [SerializeField] GameObject currentInteractableOBJ;


    //public bool playerIsInteracting;
    [SerializeField] bool interactInput;


    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    private void Update()
    {
        //Get all objects with tag
        interactableObjects = GameObject.FindGameObjectsWithTag("InteractableObject");

        bool canInteract = false;

        //check obj inside "interactAbleObjects" if is not null then check the distance of that obj.
        //If distance below "interactRange" set "canInteract = true" and put that obj to "currentInteractableOBJ"
        //And break the loop
        foreach (GameObject interactable_object in interactableObjects)
        {
            if (interactable_object != null)
            {
                if (Vector2.Distance(transform.position, interactable_object.transform.position) < interactRange)
                {
                    canInteract = true;
                    currentInteractableOBJ = interactable_object;
                    break;
                }
                else
                {
                    currentInteractableOBJ = null;
                }
            }
        }

        interactBTN.SetActive(canInteract);

        //Interacting
        if(PlayerStats.instance.isPlayerDialogue == false)
        {
            float interact_btn_value = InputManager.inputSystem.Player.Interact.ReadValue<float>();
            if (interact_btn_value > 0 && !interactInput)
            {
                Interacting();
                interactInput = true;
            }
            else if (interact_btn_value == 0)
            {
                interactInput = false;
            }

            //Debug.Log("INT VAL: " + interact_btn_value);
        }
    }

    void Interacting()
    {
        //Toggle
        PlayerStats.instance.isPlayerInteract = !PlayerStats.instance.isPlayerInteract;

        if (PlayerStats.instance.isPlayerInteract)
        {
            if (currentInteractableOBJ != null)
            {
                InteractableObject interactable_object = currentInteractableOBJ.GetComponent<InteractableObject>();
                interactable_object.isInteracted = true;
                //interactable_object.isInteracted = !interactable_object.isInteracted;
            }
            else
            {
                PlayerStats.instance.isPlayerInteract = false;
            }
        }
        else if (!PlayerStats.instance.isPlayerInteract)
        {
            if (currentInteractableOBJ != null)
            {
                InteractableObject interactable_object = currentInteractableOBJ.GetComponent<InteractableObject>();
                interactable_object.isInteracted = false;
            }
        }
    }
}
