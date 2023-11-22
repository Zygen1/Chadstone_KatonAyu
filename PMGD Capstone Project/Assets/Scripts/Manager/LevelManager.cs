using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [Header("Data")]
    public GameObject player;
    public GameObject mainCam;
    public GameObject[] envLockedRoom1;
    public GameObject[] envLockedRoom2;
    public GameObject[] envLockedRoom3;
    public GameObject[] envLockedRoom4;
    public GameObject[] envUnlockedRoom1;
    public GameObject[] envUnlockedRoom2;
    public GameObject[] envUnlockedRoom3;
    public GameObject[] envUnlockedRoom4;
    public GameObject[] items;

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
        DataManager.instance.LoadData();
        SetData();
    }

    // Update is called once per frame
    void Update()
    {
        gameOverPanel.SetActive(isGameOver);
    }

    void SetData()
    {
        player.transform.position = DataManager.instance.playerPosition;
        //mainCam.transform.position = DataManager.instance.mainCamPosition;

        if (DataManager.instance.isRoom1Unlock == true)
        {
            for (int i = 0; i < envLockedRoom1.Length; i++)
            {
                envLockedRoom1[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < envUnlockedRoom1.Length; i++)
            {
                envUnlockedRoom1[i].gameObject.SetActive(true);
            }
        }
        if (DataManager.instance.isRoom2Unlock == true)
        {
            for (int i = 0; i < envLockedRoom2.Length; i++)
            {
                envLockedRoom2[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < envUnlockedRoom2.Length; i++)
            {
                envUnlockedRoom2[i].gameObject.SetActive(true);
            }
        }
        if (DataManager.instance.isRoom3Unlock == true)
        {
            for (int i = 0; i < envLockedRoom3.Length; i++)
            {
                envLockedRoom3[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < envUnlockedRoom3.Length; i++)
            {
                envUnlockedRoom3[i].gameObject.SetActive(true);
            }
        }
        if (DataManager.instance.isRoom4Unlock == true)
        {
            for (int i = 0; i < envLockedRoom4.Length; i++)
            {
                envLockedRoom4[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < envUnlockedRoom4.Length; i++)
            {
                envUnlockedRoom4[i].gameObject.SetActive(true);
            }
        }

        /*if (DataManager.instance.itm_IsKorekApiPicked)
        {
            SetItems
        }*/

        Debug.Log("Data Set");
    }

    void SetItems(string itemName)
    {
        for (int i = 0; i < items.Length; i++)
        {
            ItemObject io = items[i].GetComponent<ItemObject>();
            if(io.itemName == itemName)
            {
                items[i].SetActive(false);
            }
        }
    }
}
