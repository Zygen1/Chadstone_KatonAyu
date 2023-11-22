using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [Header("Data")]
    public GameObject lockedRoom1;
    public GameObject lockedRoom2;
    public GameObject lockedRoom3;
    public GameObject lockedRoom4;
    public GameObject unlockedRoom1;
    public GameObject unlockedRoom2;
    public GameObject unlockedRoom3;
    public GameObject unlockedRoom4;
    public ObjectSpawner[] objectSpawners;

    [Header("Gameover")]
    public bool isGameOver;
    public GameObject gameOverPanel;

    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        //DataManager.instance.LoadData();
        //SetData();
    }

    // Update is called once per frame
    void Update()
    {
        gameOverPanel.SetActive(isGameOver);
    }

    void SetData()
    {
        lockedRoom1.SetActive(!DataManager.instance.isRoom1Unlock);
        lockedRoom2.SetActive(!DataManager.instance.isRoom2Unlock);
        lockedRoom3.SetActive(!DataManager.instance.isRoom3Unlock);
        lockedRoom4.SetActive(!DataManager.instance.isRoom4Unlock);

        unlockedRoom1.SetActive(DataManager.instance.isRoom1Unlock);
        unlockedRoom2.SetActive(DataManager.instance.isRoom2Unlock);
        unlockedRoom3.SetActive(DataManager.instance.isRoom3Unlock);
        unlockedRoom4.SetActive(DataManager.instance.isRoom4Unlock);

        if (DataManager.instance.itm_IsKorekApiPicked == false) { RespawnObject("Korek Api Spawner"); }
        if (DataManager.instance.itm_IsKertasCluePicked == false) { RespawnObject("Kertas Clue Spawner"); }
        if (DataManager.instance.itm_IsFotoPicked == false) { RespawnObject("Foto Spawner"); }
        if (DataManager.instance.itm_IsBensinPicked == false) { RespawnObject("Bensin Spawner"); }
        if (DataManager.instance.itm_IsKayuPicked == false) { RespawnObject("Kayu Spawner"); }
        if (DataManager.instance.itm_IsBingkaiPicked == false) { RespawnObject("Bingkai Spawner"); }
        if (DataManager.instance.itm_IsSlidePuzzlePiecePicked == false) { RespawnObject("Slide Puzzle Piece Spawner"); }
        if (DataManager.instance.itm_IsKeyPicked == false) { RespawnObject("Key Spawner"); }
        if (DataManager.instance.itm_IsBallPicked == false) { RespawnObject("Ball Spawner"); }
    }

    bool CheckItem(string itemName)
    {
        ItemObject[] itemObjects = GameObject.FindObjectsOfType<ItemObject>();
        for(int i = 0; i < itemObjects.Length; i++)
        {
            if(itemObjects[i].itemName == itemName)
            {
                return true;
            }
        }

        return false;
    }

    void RespawnObject(string spawnerName)
    {
        for(int i = 0; i < objectSpawners.Length; i++)
        {
            if(objectSpawners[i].spawnerName == spawnerName)
            {
                objectSpawners[i].SpawnObject();
            }
        }
    }
}
