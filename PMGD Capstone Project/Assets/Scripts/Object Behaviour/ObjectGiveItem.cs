using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractableObject))]
public class ObjectGiveItem : MonoBehaviour
{
    [SerializeField] InventoryItemData referenceItem;
    [SerializeField] bool isGiveFreeItem;


    InteractableObject interactableObject;
    bool hasGivenItem;

    // Start is called before the first frame update
    void Start()
    {
        interactableObject = GetComponent<InteractableObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGiveFreeItem)
        {
            if (interactableObject.isInteracted)
            {
                if (hasGivenItem == false)
                {
                    GiveItem();
                    hasGivenItem = true;
                }

                //interactableObject.StopInteract();
            }
        }
    }

    public void GiveItem()
    {
        InventorySystem.instance.Add(referenceItem);
        Debug.Log("Kamu dapat: " + referenceItem.name);
        NotificationUI.instance.AddObtainedItemSlot(referenceItem);
    }
}
