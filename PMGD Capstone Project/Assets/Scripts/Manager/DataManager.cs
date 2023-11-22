using UnityEngine;
using System.IO;

[System.Serializable]
public class SaveData
{
    //Room Condition
    public bool saved_IsRoom1Unlock;
    public bool saved_IsRoom2Unlock;
    public bool saved_IsRoom3Unlock;
    public bool saved_IsRoom4Unlock;

    //Item condition
    public bool saved_Itm_IsKorekApiPicked;
    public bool saved_Itm_IsKertasCluePicked;
    public bool saved_Itm_IsFotoPicked;
    public bool saved_Itm_IsBensinPicked;
    public bool saved_Itm_IsKayuPicked;
    public bool saved_Itm_IsBingkaiPicked;
    public bool saved_Itm_IsSlidePuzzlePiecePicked;
    public bool saved_Itm_IsKeyPicked;
    public bool saved_Itm_IsBallPicked;
}

[System.Serializable]
public class SavedData
{
    public string dataName;
    public bool dataCondition;
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    //Room condition
    public bool isRoom1Unlock;
    public bool isRoom2Unlock;
    public bool isRoom3Unlock;
    public bool isRoom4Unlock;

    //Item condition
    public bool itm_IsKorekApiPicked;
    public bool itm_IsKertasCluePicked;
    public bool itm_IsFotoPicked;
    public bool itm_IsBensinPicked;
    public bool itm_IsKayuPicked;
    public bool itm_IsBingkaiPicked;
    public bool itm_IsSlidePuzzlePiecePicked;
    public bool itm_IsKeyPicked;
    public bool itm_IsBallPicked;

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

        LoadData();
    }

    public void SaveData()
    {
        SaveData saveData = new SaveData();

        saveData.saved_IsRoom1Unlock = isRoom1Unlock;
        saveData.saved_IsRoom2Unlock = isRoom2Unlock;
        saveData.saved_IsRoom3Unlock = isRoom3Unlock;
        saveData.saved_IsRoom4Unlock = isRoom4Unlock;
        saveData.saved_Itm_IsKorekApiPicked = itm_IsKorekApiPicked;
        saveData.saved_Itm_IsKertasCluePicked = itm_IsKertasCluePicked;
        saveData.saved_Itm_IsFotoPicked = itm_IsFotoPicked;
        saveData.saved_Itm_IsBensinPicked = itm_IsBensinPicked;
        saveData.saved_Itm_IsKayuPicked = itm_IsKayuPicked;
        saveData.saved_Itm_IsBingkaiPicked = itm_IsBingkaiPicked;
        saveData.saved_Itm_IsSlidePuzzlePiecePicked = itm_IsSlidePuzzlePiecePicked;
        saveData.saved_Itm_IsKeyPicked = itm_IsKeyPicked;
        saveData.saved_Itm_IsBallPicked = itm_IsBallPicked;

    string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(savePath, json);
    }

    public void LoadData()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            isRoom1Unlock = saveData.saved_IsRoom1Unlock;
            isRoom2Unlock = saveData.saved_IsRoom2Unlock;
            isRoom3Unlock = saveData.saved_IsRoom3Unlock;
            isRoom4Unlock = saveData.saved_IsRoom4Unlock;
            itm_IsKorekApiPicked = saveData.saved_Itm_IsKorekApiPicked;
            itm_IsKertasCluePicked = saveData.saved_Itm_IsKertasCluePicked;
            itm_IsFotoPicked = saveData.saved_Itm_IsFotoPicked;
            itm_IsBensinPicked = saveData.saved_Itm_IsBensinPicked;
            itm_IsKayuPicked = saveData.saved_Itm_IsKayuPicked;
            itm_IsBingkaiPicked = saveData.saved_Itm_IsBingkaiPicked;
            itm_IsSlidePuzzlePiecePicked = saveData.saved_Itm_IsSlidePuzzlePiecePicked;
            itm_IsKeyPicked = saveData.saved_Itm_IsKeyPicked;
            itm_IsBallPicked = saveData.saved_Itm_IsBallPicked;
        }
    }

    public void ResetData()
    {
        isRoom1Unlock = false;
        isRoom2Unlock = false;
        isRoom3Unlock = false;
        isRoom4Unlock = false;
        isRoom1Unlock = false;
        isRoom2Unlock = false;
        isRoom3Unlock = false;
        isRoom4Unlock = false;
        itm_IsKorekApiPicked = false;
        itm_IsKertasCluePicked = false;
        itm_IsFotoPicked = false;
        itm_IsBensinPicked = false;
        itm_IsKayuPicked = false;
        itm_IsBingkaiPicked = false;
        itm_IsSlidePuzzlePiecePicked = false;
        itm_IsKeyPicked = false;
        itm_IsBallPicked = false;
        SaveData();
    }
}
