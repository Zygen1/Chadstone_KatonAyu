using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem instance;

    public GameObject itemParent;

    public List<InventoryItem> inventory { get; private set; }
    public event Action onInventoryChangeEvent;

    private Dictionary<InventoryItemData, InventoryItem> m_itemDictionary;

    private void Awake()
    {
        instance = this;
        inventory = new List<InventoryItem> ();
        m_itemDictionary = new Dictionary<InventoryItemData, InventoryItem> ();
    }

    public InventoryItem Get(InventoryItemData referenceData)
    {
        if(m_itemDictionary.TryGetValue (referenceData, out InventoryItem value))
        {
            return value;
        }

        return null;
    }

    public void Add(InventoryItemData referenceData)
    {
        if(m_itemDictionary.TryGetValue(referenceData, out InventoryItem value))
        {
            value.AddToStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(referenceData);
            inventory.Add(newItem);
            m_itemDictionary.Add(referenceData, newItem);
        }

        if (onInventoryChangeEvent != null)
        {
            onInventoryChangeEvent.Invoke();
        }

    }

    public void Remove(InventoryItemData referenceData)
    {
        if(m_itemDictionary.TryGetValue(referenceData, out InventoryItem value))
        {
            value.RemoveFromStack();

            if(value.stackSize == 0)
            {
                inventory.Remove(value);
                m_itemDictionary.Remove(referenceData);
            }
        }

        if (onInventoryChangeEvent != null)
        {
            onInventoryChangeEvent.Invoke();
        }

    }

    [ContextMenu("Check List Inventory")]
    void DebugListInventory()
    {
        for(int i = 0; i < inventory.Count; i++)
        {
            Debug.Log("Item " + i + " : " + inventory[i]);
        }
    }

    public bool SearchItemInInventory(string itemName)
    {
        foreach (Transform gameObject in itemParent.transform)
        {
            ItemSlot itemSlot = gameObject.GetComponent<ItemSlot>();
            if (itemSlot.itemName == itemName)
            {
                return true;
            }
        }

        return false;
    }

    public InventoryItem GetReferenceItemDataInInventory(string itemName)
    {
        foreach (Transform gameObject in itemParent.transform)
        {
            ItemSlot itemSlot = gameObject.GetComponent<ItemSlot>();
            if (itemSlot.itemName == itemName)
            {
                return itemSlot.inventoryItem;
            }
        }

        return null;
    }
}

[System.Serializable]
public class InventoryItem
{
    public InventoryItemData data { get; private set; }
    public int stackSize { get; private set; }

    public InventoryItem(InventoryItemData source)
    {
        data = source;
        AddToStack();
    }

    public void AddToStack()
    {
        stackSize++;
    }

    public void RemoveFromStack()
    {
        stackSize--;
    }
}


