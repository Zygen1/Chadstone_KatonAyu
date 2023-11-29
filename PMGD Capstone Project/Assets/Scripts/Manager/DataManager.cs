using UnityEngine;
using System.Collections.Generic;
using System.IO;

[System.Serializable]
public class SaveData
{
    //Player Pos
    public Vector3 saved_PlayerPosition;
    //public Vector3 saved_MainCamPosition;

    //Room Condition
    public bool saved_IsRoom1Unlock;
    public bool saved_IsRoom2Unlock;
    public bool saved_IsRoom3Unlock;
    public bool saved_IsRoom4Unlock;

    //Ending
    public bool saved_SecretEndingUnlock;
    public bool saved_GoodEndingUnlock;

    public List<ItemData> saved_ItemPickedStatus;
    public List<ItemData> saved_ItemUsedStatus;
}

[System.Serializable]
public class ItemData
{
    public string key;
    public bool value;
}


public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    //Player pos
    public Vector3 playerPosition;

    //Room condition
    public bool isRoom1Unlock;
    public bool isRoom2Unlock;
    public bool isRoom3Unlock;
    public bool isRoom4Unlock;

    //Ending
    public bool secretEndingUnlock;
    public bool goodEndingUnlock;

    public Dictionary<string, bool> itemPickedStatus;
    public Dictionary<string, bool> itemUsedStatus;

    private string savePath;

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
        DontDestroyOnLoad(this);

        savePath = Path.Combine(Application.persistentDataPath, "savedata.json");
    }

    [ContextMenu("Save")]
    public void SaveData()
    {
        SaveData saveData = new SaveData();

        saveData.saved_PlayerPosition = playerPosition;
        saveData.saved_IsRoom1Unlock = isRoom1Unlock;
        saveData.saved_IsRoom2Unlock = isRoom2Unlock;
        saveData.saved_IsRoom3Unlock = isRoom3Unlock;
        saveData.saved_IsRoom4Unlock = isRoom4Unlock;

        saveData.saved_SecretEndingUnlock = secretEndingUnlock;
        saveData.saved_GoodEndingUnlock = goodEndingUnlock;

        saveData.saved_ItemPickedStatus = ConvertDictionaryToList(itemPickedStatus);
        saveData.saved_ItemUsedStatus = ConvertDictionaryToList(itemUsedStatus);

        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(savePath, json);
    }

    [ContextMenu("Load")]
    public void LoadData()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            playerPosition = saveData.saved_PlayerPosition;
            isRoom1Unlock = saveData.saved_IsRoom1Unlock;
            isRoom2Unlock = saveData.saved_IsRoom2Unlock;
            isRoom3Unlock = saveData.saved_IsRoom3Unlock;
            isRoom4Unlock = saveData.saved_IsRoom4Unlock;

            secretEndingUnlock = saveData.saved_SecretEndingUnlock;
            goodEndingUnlock = saveData.saved_GoodEndingUnlock;

            itemPickedStatus = ConvertListToDictionary(saveData.saved_ItemPickedStatus);
            itemUsedStatus = ConvertListToDictionary(saveData.saved_ItemUsedStatus);
        }
        else
        {
            itemPickedStatus = new Dictionary<string, bool>();
            itemUsedStatus = new Dictionary<string, bool>();
        }
    }


    [ContextMenu("Reset Data")]
    public void ResetData()
    {
        playerPosition = new Vector3(0.45f, 3.95f, 0);
        isRoom1Unlock = false;
        isRoom2Unlock = false;
        isRoom3Unlock = false;
        isRoom4Unlock = false;
        secretEndingUnlock = false;
        goodEndingUnlock = false;

        if (itemPickedStatus != null)
        {
            itemPickedStatus.Clear();
        }
        else
        {
            itemPickedStatus = new Dictionary<string, bool>();
        }

        if (itemUsedStatus != null)
        {
            itemUsedStatus.Clear();
        }
        else
        {
            itemUsedStatus = new Dictionary<string, bool>();
        }
        SaveData();
        Debug.Log("Data Reset");
    }

    [ContextMenu("Display Dictionary")]
    public void DisplayItemStatus()
    {
        Debug.Log("Item Picked Status:");
        foreach (KeyValuePair<string, bool> pair in itemPickedStatus)
        {
            Debug.Log(pair.Key + ": " + pair.Value);
        }

        Debug.Log("Item Used Status:");
        foreach (KeyValuePair<string, bool> pair in itemUsedStatus)
        {
            Debug.Log(pair.Key + ": " + pair.Value);
        }
    }

    private List<ItemData> ConvertDictionaryToList(Dictionary<string, bool> dictionary)
    {
        List<ItemData> list = new List<ItemData>();
        foreach (KeyValuePair<string, bool> pair in dictionary)
        {
            ItemData itemData = new ItemData();
            itemData.key = pair.Key;
            itemData.value = pair.Value;
            list.Add(itemData);
        }
        return list;
    }

    private Dictionary<string, bool> ConvertListToDictionary(List<ItemData> list)
    {
        Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
        foreach (ItemData itemData in list)
        {
            dictionary[itemData.key] = itemData.value;
        }
        return dictionary;
    }
}
