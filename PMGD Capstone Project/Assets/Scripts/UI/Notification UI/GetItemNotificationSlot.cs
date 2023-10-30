using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GetItemNotificationSlot : MonoBehaviour
{
    [Header("Atribut")]
    public string itemName;

    [Header("Requirement")]
    public InventoryItemData inventoryItem;
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI label;

    [Header("Life Time")]
    public float lifeTime;
    private float timer;

    void Start()
    {
        timer = lifeTime;
    }

    void Update()
    {
        if (timer <= 0)
        {
            Destroy(gameObject);
        }

        timer -= Time.deltaTime;
    }

    public void Set(InventoryItemData item)
    {
        inventoryItem = item;
        itemName = inventoryItem.displayName;
        icon.sprite = item.icon;
        label.text = item.displayName;
    }
}
