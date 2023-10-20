using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityPuzzle : MonoBehaviour
{
    static public ElectricityPuzzle Instance;

    public int switchCount;
    private int onCount = 0;

    [Header("Debug")]
    [SerializeField] PuzzleStats puzzleStats;

    private void Awake()
    {
        puzzleStats = GetComponent<PuzzleStats>();
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchChange(int points)
    {
        onCount = onCount + points;
        if (onCount == switchCount)
        {
            puzzleStats.isDone = true;
        }
    }
}
