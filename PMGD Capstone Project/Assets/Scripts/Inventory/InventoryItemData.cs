using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Inventory Item Data")]
public class InventoryItemData : ScriptableObject
{
    public string id;
    public string displayName;
    public Sprite icon;
    public GameObject prefab;
    public InventoryItemData referenceData;

    public bool isUseEvent;
    public UnityEvent itemEvent;
    public bool dontDestroyWhenUse;

    public bool isAnEquipment;
    public string equipmentName;

    [Header("DUMMY")]
    public bool isADummy;
}
