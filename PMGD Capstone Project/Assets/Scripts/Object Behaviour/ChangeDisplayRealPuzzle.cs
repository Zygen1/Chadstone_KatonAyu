using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PuzzleStats))]
public class ChangeDisplayRealPuzzle : MonoBehaviour
{
    [SerializeField] GameObject lockedDisplay;
    [SerializeField] GameObject unlockedDisplay;

    PuzzleStats stats;

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<PuzzleStats>();
    }

    // Update is called once per frame
    void Update()
    {
        lockedDisplay.SetActive(!stats.isDone);
        unlockedDisplay.SetActive(stats.isDone);
    }
}
