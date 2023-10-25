using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationLockPuzzle : MonoBehaviour
{
    public int[] password;

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
        }
        return true;
    }
}
