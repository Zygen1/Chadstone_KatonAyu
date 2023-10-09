using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyItemInventory : MonoBehaviour
{
    [SerializeField] InventoryItem inventoryItem;

    public void DestroyItem(string item_name)
    {
        inventoryItem = InventorySystem.instance.GetReferenceItemDataInInventory(item_name);
        InventorySystem.instance.Remove(inventoryItem.data.referenceData);
    }
}
