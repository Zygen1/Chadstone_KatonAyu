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
    [HideInInspector] public DialogueTrigger otherDialogueTrig;
    [HideInInspector] public bool forceStart;

    [Header("Activate Obj")]
    [HideInInspector] public bool isActivateObj;
    [HideInInspector] public GameObject objToActivate;
    [Header("TEMP Header (isActivateObj?)")]
    public GameObject[] otherObjToActivate;
    public GameObject[] otherObjToDeactivate;

    [Header("Give Item")]
    [HideInInspector] public bool isGiveItem;
    [HideInInspector] public ObjectGiveItem objGiveItem;

    [Header("Show Dialogue")]
    public bool isShowingDialogue;
    public DialogueTrigger dialogueTrigger;

    [Header("Change to Untagged")]
    public bool isChangeToUntagged;

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
                otherDialogueTrig = GetComponentInParent<DialogueTrigger>();
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

            /*interactableObject.StopInteract();*/
            if (!isShowingDialogue || isDone)
            {
                interactableObject.StopInteract();
            }
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
        if (isShowingDialogue)
        {
            dialogueTrigger.enabled = false;
            Debug.Log("DISABLE DIALOGUE");
        }

        if (isChangeDialogue)
        {
            otherDialogueTrig.currentDialogue++;
            if (forceStart)
            {
                otherDialogueTrig.TriggerDialogue();
            }
        }

        if (isActivateObj)
        {
            objToActivate.SetActive(true);

            for(int i = 0; i < otherObjToActivate.Length; i++)
            {
                otherObjToActivate[i].SetActive(true);
            }

            for (int i = 0; i < otherObjToDeactivate.Length; i++)
            {
                otherObjToDeactivate[i].SetActive(false);
            }
        }

        if (isASwitchObj)
        {
            switchObject.isOn = true;
        }

        if (isGiveItem)
        {
            objGiveItem.GiveItem();
        }

        if (isChangeToUntagged)
        {
            this.gameObject.tag = "Untagged";
        }

        isDone = true;
        //interactableObject.StopInteract();
    }
}
