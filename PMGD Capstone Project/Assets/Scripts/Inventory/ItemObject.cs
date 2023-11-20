using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractableObject))]
public class ItemObject : MonoBehaviour
{
    public InventoryItemData referenceItem;
    InteractableObject interactableObject;

    public bool onClickPick;

    [Header("SetObject")]
    public bool isSetObject;
    public GameObject[] deactiveObj;
    public GameObject[] activateObj;

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
        NotificationUI.instance.AddObtainedItemSlot(referenceItem);

        if (isSetObject)
        {
            SetObject();
        }

        Destroy(gameObject);
    }

    public void OnMouseDown()
    {
        if (onClickPick)
        {
            Debug.Log("CLICKED");
            InventorySystem.instance.Add(referenceItem);
            NotificationUI.instance.AddObtainedItemSlot(referenceItem);

            if (isSetObject)
            {
                SetObject();
            }

            Destroy(gameObject);
        }
    }

    void SetObject()
    {
        for(int i = 0; i < deactiveObj.Length; i++)
        {
            deactiveObj[i].SetActive(false);
        }

        for (int i = 0; i < activateObj.Length; i++)
        {
            activateObj[i].SetActive(true);
        }

        Debug.Log("SetObject");
    }
}
