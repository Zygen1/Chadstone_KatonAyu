using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractableObject))]
public class ObjectNeedItem : MonoBehaviour
{
    [Header("Option")]
    [HideInInspector] public string itemName;
    [HideInInspector] public bool destroyItem;
    [HideInInspector] public bool isASwitchObj;
    [HideInInspector] public SwitchObject switchObject;

    [Header("Change Dialogue")]
    [HideInInspector] public bool isChangeDialogue;
    [HideInInspector] public bool isParentDialogue;
    [HideInInspector] public DialogueTrigger dialogueTrigger;
    [HideInInspector] public bool forceStart;

    [Header("Activate Obj")]
    [HideInInspector] public bool isActivateObj;
    [HideInInspector] public GameObject objToActivate;

    [Header("Requirement")]
    private InteractableObject interactableObject;

    [Header("Status")]
    private bool isDone;

    private void Start()
    {
        interactableObject = GetComponent<InteractableObject>();

        if (isChangeDialogue)
        {
            if (isParentDialogue)
            {
                dialogueTrigger = GetComponentInParent<DialogueTrigger>();
            }
        }

        if (isASwitchObj)
        {
            switchObject = GetComponent<SwitchObject>();
        }
    }

    private void Update()
    {
        if (!isDone)
        {
            if (interactableObject.isInteracted)
            {
                if (InventorySystem.instance.SearchItemInInventory(itemName))
                {
                    HandleItemFound();
                }
                else
                {
                    Debug.Log("Item not found");
                }
            }
        }

        interactableObject.StopInteract();
    }

    private void HandleItemFound()
    {
        if (destroyItem)
        {
            InventoryItem inventoryItem = InventorySystem.instance.GetReferenceItemDataInInventory(itemName);
            InventorySystem.instance.Remove(inventoryItem.data.referenceData);
        }

        if (isChangeDialogue)
        {
            dialogueTrigger.currentDialogue++;
            if (forceStart)
            {
                dialogueTrigger.TriggerDialogue();
            }
        }

        if (isActivateObj)
        {
            objToActivate.SetActive(true);
        }

        if (isASwitchObj)
        {
            switchObject.isOn = true;
        }

        isDone = true;
    }
}
