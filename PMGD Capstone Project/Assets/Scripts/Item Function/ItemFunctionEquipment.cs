using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFunctionEquipment : MonoBehaviour
{
    bool isEquip;

    public void EquipAndUnequip(string equipmentName)
    {
        isEquip = !isEquip;
        EquipmentManager.instance.SetEquipment(equipmentName, isEquip);
    }
}
