using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] GameObject slotPrefab;
    /*[SerializeField] GameObject slotParent;*/
    [SerializeField] GameObject itemSlotParent;
    [SerializeField] GameObject equipmentSlotParent;

    // Start is called before the first frame update
    void Start()
    {
        InventorySystem.instance.onInventoryChangeEvent += OnUpdateInventory;
    }

    void OnUpdateInventory()
    {
        /*foreach (Transform t in slotParent.transform)
        {
            Destroy(t.gameObject);
        }*/

        foreach (Transform t in itemSlotParent.transform)
        {
            Destroy(t.gameObject);
        }

        foreach (Transform t in equipmentSlotParent.transform)
        {
            Destroy(t.gameObject);
        }

        DrawInventory();
    }

    public void DrawInventory()
    {
        foreach(InventoryItem item in InventorySystem.instance.inventory)
        {
            AddInventorySlot(item);
        }
    }

    public void AddInventorySlot(InventoryItem item)
    {
        GameObject obj = Instantiate(slotPrefab);
        //obj.transform.SetParent(slotParent.transform, false);

        if (item.data.isAnEquipment)
        {
            obj.transform.SetParent(equipmentSlotParent.transform, false);
        }
        else
        {
            obj.transform.SetParent(itemSlotParent.transform, false);
        }

        ItemSlot slot = obj.GetComponent<ItemSlot>();
        slot.Set(item);

    }
}
