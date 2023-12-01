using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTrigger : MonoBehaviour
{
    public GameObject saveIndicator;

    [Header("Choose One")]
    [SerializeField] bool room1Clear;
    [SerializeField] bool room2Clear;
    [SerializeField] bool room3Clear;
    [SerializeField] bool room4Clear;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
            Vector3 mainCamPos = GameObject.FindGameObjectWithTag("MainCamera").transform.position;

            DataManager.instance.playerPosition = playerPos;
            //DataManager.instance.mainCamPosition = mainCamPos;
            if (room1Clear) { DataManager.instance.isRoom1Unlock = true; }
            if (room2Clear) { DataManager.instance.isRoom2Unlock = true; }
            if (room3Clear) { DataManager.instance.isRoom3Unlock = true; }
            if (room4Clear) { DataManager.instance.isRoom4Unlock = true; }

            DataManager.instance.SaveData();
            saveIndicator.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
