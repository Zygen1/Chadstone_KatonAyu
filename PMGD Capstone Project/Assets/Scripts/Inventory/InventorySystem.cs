using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem instance;

    public GameObject itemParent;
    public InventoryItemData[] startingItems;

    public List<InventoryItem> inventory { get; private set; }
    public event Action onInventoryChangeEvent;

    private Dictionary<InventoryItemData, InventoryItem> m_itemDictionary;

    [Header("DUMMY")]
    [SerializeField] InventoryItemData dummyItemData;

    private void Awake()
    {
        instance = this;
        inventory = new List<InventoryItem>();
        m_itemDictionary = new Dictionary<InventoryItemData, InventoryItem>();
    }

    private void Start()
    {
        Add(dummyItemData);
        StartCoroutine(DelayStartingItem());
    }

    IEnumerator DelayStartingItem()
    {
        Debug.Log("Coroutine Start");
        yield return new WaitForSeconds(0.1f);
        AddStartingItems();
        Debug.Log("Coroutine End");
    }

    public InventoryItem Get(InventoryItemData referenceData)
    {
        if (m_itemDictionary.TryGetValue(referenceData, out InventoryItem value))
        {
            return value;
        }

        return null;
    }

    public void Add(InventoryItemData referenceData)
    {
        if (m_itemDictionary.TryGetValue(referenceData, out InventoryItem value))
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

        //Debug.Log("ItemAdded");
    }

    public void Remove(InventoryItemData referenceData)
    {
        if (m_itemDictionary.TryGetValue(referenceData, out InventoryItem value))
        {
            value.RemoveFromStack();

            if (value.stackSize == 0)
            {
                inventory.Remove(value);
                m_itemDictionary.Remove(referenceData);
            }
        }

        if (onInventoryChangeEvent != null)
        {
            onInventoryChangeEvent.Invoke();
        }

        DataItemIsUsed(referenceData);
    }

    void DataItemIsUsed(InventoryItemData referenceData)
    {
        string itemName = referenceData.displayName;

        // Pastikan kunci sudah ada atau tambahkan jika belum ada
        if (!DataManager.instance.itemUsedStatus.ContainsKey(itemName))
        {
            DataManager.instance.itemUsedStatus.Add(itemName, true); // Tambahkan kunci dengan nilai true
        }
        else
        {
            DataManager.instance.itemUsedStatus[itemName] = true; // Update nilai jika kunci sudah ada
        }
    }

    [ContextMenu("Check List Inventory")]
    void DebugListInventory()
    {
        for (int i = 0; i < inventory.Count; i++)
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

    [ContextMenu("Add Starting Items")]
    public void AddStartingItems()
    {
        if (startingItems != null && startingItems.Length > 0)
        {
            for (int i = 0; i < startingItems.Length; i++)
            {
                Add(startingItems[i]);
            }
        }
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


