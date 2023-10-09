using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractableObject))]
public class ItemObject : MonoBehaviour
{
    public InventoryItemData referenceItem;
    InteractableObject interactableObject;

    private void Awake()
    {
        interactableObject = GetComponent<InteractableObject>();
    }

    private void Update()
    {
        if (interactableObject.isInteracted)
        {
            OnHandlePickupItem();
        }
    }

    [ContextMenu("Pick")]
    public void OnHandlePickupItem()
    {
        InventorySystem.instance.Add(referenceItem);
        PlayerStats.instance.isPlayerInteract = false;
        Destroy(gameObject);
    }
}
