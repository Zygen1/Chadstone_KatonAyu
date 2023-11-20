using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectPowerPuzzle : MonoBehaviour
{
    public static ConnectPowerPuzzle instance;

    public GameObject activateObject;
    public GameObject deactivateObject;
    public GameObject rotateObject;
    public bool connected = false;

    [Header("Debug")]
    [SerializeField] PuzzleStats puzzleStats;

    private void Awake()
    {
        puzzleStats = GetComponent<PuzzleStats>();
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (connected)
        {
            puzzleStats.isDone = true;
        }
    }

    /*public bool PowerConnected()
    {
        connected = true;

        if (connected)
        {
            deactivateObject.SetActive(false);
            activateObject.SetActive(true);
            rotateObject.transform.Rotate(0f, 0f, 90f);
            return true;
        }
        else
        {
            return false;
        }
    }*/
}
