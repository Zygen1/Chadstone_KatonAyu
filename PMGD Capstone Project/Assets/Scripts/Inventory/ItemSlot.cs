using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour
{
    [Header("Atribut")]
    public string itemName;

    [Header("Requirement")]
    public InventoryItem inventoryItem;
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI label;
    [SerializeField] GameObject stackOBJ;
    [SerializeField] TextMeshProUGUI stackLabel;

    [SerializeField] GameObject currentEquipmentObject;
    [SerializeField] GameObject equipmentStatus;
    [SerializeField] GameObject equipStatus;
    [SerializeField] GameObject unequipStatus;

    [Header("Debug")]
    [SerializeField] bool isEquip;

    public void Set(InventoryItem item)
    {
        inventoryItem = item;
        itemName = inventoryItem.data.displayName;
        icon.sprite = item.data.icon;
        label.text = item.data.displayName;

        for (int i = 0; i < EquipmentManager.instance.equipmentList.Length; i++)
        {
            EquipmentObject equipmentObject = EquipmentManager.instance.equipmentList[i].GetComponent<EquipmentObject>();
            if (equipmentObject.equipmentObjectName == inventoryItem.data.equipmentName)
            {
                currentEquipmentObject = EquipmentManager.instance.equipmentList[i];
            }
        }

        equipmentStatus.SetActive(inventoryItem.data.isAnEquipment);

        if (item.stackSize <= 1)
        {
            stackOBJ.SetActive(false);
            return;
        }

        stackLabel.text = item.stackSize.ToString();
    }

    public void UseItem()
    {
        if (inventoryItem.data.isUseEvent || inventoryItem.data.isAnEquipment)
        {
            if (!inventoryItem.data.isAnEquipment)
            {
                inventoryItem.data.itemEvent?.Invoke();
                InventorySystem.instance.Remove(inventoryItem.data.referenceData);
            }
            else if (inventoryItem.data.isAnEquipment)
            {
                UseEquipment();
            }
        }

        Debug.Log("Item used");
    }

    void UseEquipment()
    {
        if(EquipmentManager.instance.currentActiveEquipment == null || EquipmentManager.instance.currentActiveEquipment.GetComponent<EquipmentObject>().equipmentObjectName == inventoryItem.data.equipmentName)
        {
            isEquip = !isEquip;
            equipStatus.SetActive(isEquip);
            unequipStatus.SetActive(!isEquip);
            currentEquipmentObject.SetActive(isEquip);
            Debug.Log("Use Equipment");
        }
    }
}
