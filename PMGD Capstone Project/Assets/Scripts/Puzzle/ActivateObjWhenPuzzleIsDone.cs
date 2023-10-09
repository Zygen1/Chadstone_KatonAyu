using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObjWhenPuzzleIsDone : MonoBehaviour
{
    [SerializeField] GameObject objToSetActive;

    PuzzleStats stats;
    bool activateIsDone;

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<PuzzleStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stats.isDone && !activateIsDone)
        {
            objToSetActive.SetActive(true);
            activateIsDone = true;
        }
    }
}
