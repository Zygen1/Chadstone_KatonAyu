using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [Header("Data")]
    public GameObject player;
    public GameObject mainCam;
    public GameObject[] environmmentLockedRoom1;
    public GameObject[] environmmentLockedRoom2;
    public GameObject[] environmmentLockedRoom3;
    public GameObject[] environmmentLockedRoom4;
    public GameObject[] environmmentUnlockedRoom1;
    public GameObject[] environmmentUnlockedRoom2;
    public GameObject[] environmmentUnlockedRoom3;
    public GameObject[] environmmentUnlockedRoom4;
    public GameObject[] items;
    public InventoryItemData[] itemsData;

    [Header("Gameover")]
    public bool isGameOver1;
    public bool isGameOver2;
    public GameObject gameOverPanel1;
    public GameObject gameOverPanel2;

    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        DataManager.instance.LoadData();
        SetData();
    }

    // Update is called once per frame
    void Update()
    {
        gameOverPanel1.SetActive(isGameOver1);
        gameOverPanel2.SetActive(isGameOver2);
    }

    [ContextMenu("SetData")]
    void SetData()
    {
        player.transform.position = DataManager.instance.playerPosition;

        //Room
        if (DataManager.instance.isRoom1Unlock == true)
        {
            for (int i = 0; i < environmmentLockedRoom1.Length; i++) { environmmentLockedRoom1[i].gameObject.SetActive(false); }
            for (int i = 0; i < environmmentUnlockedRoom1.Length; i++) { environmmentUnlockedRoom1[i].gameObject.SetActive(true); }
        }
        if (DataManager.instance.isRoom2Unlock == true)
        {
            for (int i = 0; i < environmmentLockedRoom2.Length; i++) { environmmentLockedRoom2[i].gameObject.SetActive(false); }
            for (int i = 0; i < environmmentUnlockedRoom2.Length; i++) { environmmentUnlockedRoom2[i].gameObject.SetActive(true); }
        }
        if (DataManager.instance.isRoom3Unlock == true)
        {
            for (int i = 0; i < environmmentLockedRoom3.Length; i++) { environmmentLockedRoom3[i].gameObject.SetActive(false); }
            for (int i = 0; i < environmmentUnlockedRoom3.Length; i++) { environmmentUnlockedRoom3[i].gameObject.SetActive(true); }
        }
        if (DataManager.instance.isRoom4Unlock == true)
        {
            for (int i = 0; i < environmmentLockedRoom4.Length; i++) { environmmentLockedRoom4[i].gameObject.SetActive(false); }
            for (int i = 0; i < environmmentUnlockedRoom4.Length; i++) { environmmentUnlockedRoom4[i].gameObject.SetActive(true); }
        }

        //Items
        for (int i = 0; i < itemsData.Length; i++)
        {
            string itemNameToCheck = itemsData[i].displayName; // Nama item yang ingin Anda cek

            if (DataManager.instance.itemPickedStatus.ContainsKey(itemNameToCheck))
            {
                bool isItemPicked = DataManager.instance.itemPickedStatus[itemNameToCheck];
                bool isItemUsed = DataManager.instance.itemUsedStatus.ContainsKey(itemNameToCheck) ? DataManager.instance.itemUsedStatus[itemNameToCheck] : false;

                if (isItemPicked && !isItemUsed)
                {
                    SetItems(itemNameToCheck);
                    SetInventory(itemNameToCheck);
                }
                else if (isItemPicked && isItemUsed)
                {
                    SetItems(itemNameToCheck);
                }
                else
                {
                    Debug.Log(itemNameToCheck + " belum dipilih");
                }
            }
        }

        Debug.Log("Data Set");
    }

    void SetItems(string itemName)
    {
        for (int i = 0; i < items.Length; i++)
        {
            ItemObject io = items[i].GetComponent<ItemObject>();
            if (io.referenceItem.displayName == itemName) { items[i].gameObject.SetActive(false); }
        }
    }

    void SetInventory(string itemName)
    {
        for (int i = 0; i < itemsData.Length; i++)
        {
            if (itemsData[i].displayName == itemName)
            {
                InventorySystem.instance.Add(itemsData[i]);
            }
        }
    }
}
