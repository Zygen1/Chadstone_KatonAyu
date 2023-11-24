using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationUI
    : MonoBehaviour
{
    public static NotificationUI instance;

    [Header("Notification When Need an Item")]
    [SerializeField] GameObject notificationNeedItem;
    [SerializeField] GameObject notificationItemParent;
    private GameObject notificationNeedItemObj;

    [Header("Notification When Obtained New Item")]
    [SerializeField] GameObject slotPrefab;
    [SerializeField] GameObject notificationSlotParent;

    [Header("Audio")]
    [SerializeField] AudioSource audioSource;

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

    void Start()
    {
        
    }

    public void ShowNotification(string item)
    {
        if (notificationItemParent.transform.childCount > 0)
        {
            Time.timeScale = 1.0f;
            for (var i = notificationItemParent.transform.childCount - 1; i >= 0; i--)
            {
                Destroy(notificationItemParent.transform.GetChild(i).gameObject);
            }
        }

        notificationNeedItemObj = Instantiate(notificationNeedItem);
        notificationNeedItemObj.transform.SetParent(notificationItemParent.transform, false);
        Animator animator = notificationNeedItemObj.GetComponent<Animator>();
        animator.SetBool("IsIn", true);
        NeedItemNotificationSlot slot = notificationNeedItemObj.GetComponent<NeedItemNotificationSlot>();
        slot.Set(item);
    }

    public void CloseNotification()
    {
        Animator animator = notificationNeedItemObj.GetComponent<Animator>();
        animator.SetBool("IsIn", false);
        Time.timeScale = 1.0f;
    }

    public void AddObtainedItemSlot(InventoryItemData item)
    {
        if(audioSource != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.Log("OBJ INI TIDAK ADA AUDIO: " + gameObject.name);
        }

        GameObject obj = Instantiate(slotPrefab);
        obj.transform.SetParent(notificationSlotParent.transform, false);

        GetItemNotificationSlot slot = obj.GetComponent<GetItemNotificationSlot>();
        slot.Set(item);
    }
}
