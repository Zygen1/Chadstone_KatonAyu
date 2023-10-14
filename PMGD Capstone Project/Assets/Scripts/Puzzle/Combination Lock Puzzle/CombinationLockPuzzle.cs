using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationLockPuzzle : MonoBehaviour
{
    public int[] password;
    //public GameObject[] dials;

    //[SerializeField] private int[] currentCombination;

    [SerializeField] DialsValue[] dialsValue;

    [Header("Debug")]
    [SerializeField] PuzzleStats puzzleStats;

    private void Awake()
    {
        puzzleStats = GetComponent<PuzzleStats>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //currentCombination = new int[dials.Length];
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckCombination())
        {
            puzzleStats.isDone = true;
            Debug.Log("bisa");
        }
    }

    private bool CheckCombination()
    {
        for (int i = 0; i < 3; i++)
        {
            if (dialsValue[i].dialsValue != password[i])
            {
                return false;
            }
            
            /*if (currentCombination[i] != password[i])
            {
                return false;
            }*/
        }
        return true;
    }

    internal void SetDialValue(int dialsValue)
    {
        
    }
}
