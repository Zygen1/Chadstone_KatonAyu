using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AfterGetItemAction { NONE, CHANGE_DIALOGUE}

[RequireComponent(typeof(InteractableObject))]
public class ObjectNeedItem : MonoBehaviour
{
    [SerializeField] string itemName;
    [SerializeField] bool destroyItem;
    [SerializeField] DestroyItemInventory destroyItemInventory;
    [SerializeField] bool destroyThisObj;

    
    [SerializeField] AfterGetItemAction afterGetItemAction;
    [Tooltip("Force start dialogue after obj get item")]
    [SerializeField] bool forceStart;
    
    [Header("Requirement")]
    InteractableObject interactableObject;
    //DialogueHandler dialogueHandler;
    DialogueTrigger dialogueTrigger;

    // Start is called before the first frame update
    void Start()
    {
        interactableObject = GetComponent<InteractableObject>();

        if(afterGetItemAction == AfterGetItemAction.CHANGE_DIALOGUE)
        {
            //dialogueHandler = GetComponentInParent<DialogueHandler>();
            dialogueTrigger = GetComponentInParent<DialogueTrigger>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (interactableObject.isInteracted)
        {
            if(InventorySystem.instance.SearchItemInInventory(itemName) == true)
            {
                if (destroyItem)
                {
                    destroyItemInventory.DestroyItem(itemName);
                }

                if (afterGetItemAction == AfterGetItemAction.CHANGE_DIALOGUE)
                {
                    dialogueTrigger.currentDialogue++;
                    if (forceStart)
                    {
                        dialogueTrigger.TriggerDialogue();
                    }
                }

                if (destroyThisObj)
                {
                    Destroy(gameObject);
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
            else
            {
                Debug.Log("Item not found");
            }

            interactableObject.isInteracted = false;
            PlayerStats.instance.isPlayerInteract = false;
        }
    }
}
