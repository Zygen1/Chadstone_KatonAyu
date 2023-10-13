using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialsValue : MonoBehaviour
{
    public int dialsValue;
    public CombinationLockPuzzle combinationLock;

    [SerializeField] GameObject[] dialNumber;

    PuzzleStats puzzleStats;

    // Start is called before the first frame update
    void Start()
    {
        puzzleStats = GetComponentInParent<PuzzleStats>();
    }

    // Update is called once per frame
    void Update()
    {
        dialNumber[dialsValue].SetActive(true);

        for (int i = 0; i < dialNumber.Length; i++)
        {
            if (i != dialsValue)
            {
                dialNumber[i].SetActive(false);
            }
        }
    }

    private void OnMouseDown()
    {
        dialsValue++;

        if (dialsValue > 9)
        {
            dialsValue = 0;
        }

        combinationLock.SetDialValue(dialsValue);
        Debug.Log("pencet");
    }
}
