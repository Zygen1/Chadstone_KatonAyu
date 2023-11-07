using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractableObject))]
public class ObjectNeedItem : MonoBehaviour
{
    [Header("Option")]
    [HideInInspector] public string[] itemName;
    private int itemUnlock;
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
    [HideInInspector] private InteractableObject interactableObject;

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
        if (interactableObject.isInteracted)
        {
            if (!isDone)
            {
                HandleSearchItem();
            }

            interactableObject.StopInteract();
        }
    }

    private void HandleSearchItem()
    {
        for (int i = 0; i < itemName.Length; i++)
        {
            if (InventorySystem.instance.SearchItemInInventory(itemName[i]) == true)
            {
                itemUnlock++;
                if (destroyItem)
                {
                    InventoryItem inventoryItem = InventorySystem.instance.GetReferenceItemDataInInventory(itemName[i]);
                    InventorySystem.instance.Remove(inventoryItem.data.referenceData);
                }

                itemName[i] = null;
            }
        }

        if (itemUnlock == itemName.Length)
        {
            HandleItemFound();
        }
        else
        {
            //NOTIFIKASI ///////////////////////////////////////////////////////
            Debug.Log("Need Item");
            //END NOTIFIKASI ///////////////////////////////////////////////////


            //interactableObject.StopInteract();
        }
    }

    private void HandleItemFound()
    {
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
        //interactableObject.StopInteract();
    }
}
