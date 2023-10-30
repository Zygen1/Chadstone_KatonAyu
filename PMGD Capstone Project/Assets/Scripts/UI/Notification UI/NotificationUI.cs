using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class NotificationUI
    : MonoBehaviour
{
    public static NotificationUI instance;

    [SerializeField] GameObject slotPrefab;
    [SerializeField] GameObject notificationSlotParent;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void AddObtainedItemSlot(InventoryItemData item)
    {
        GameObject obj = Instantiate(slotPrefab);
        obj.transform.SetParent(notificationSlotParent.transform, false);

        GetItemNotificationSlot slot = obj.GetComponent<GetItemNotificationSlot>();
        slot.Set(item);
    }
}
