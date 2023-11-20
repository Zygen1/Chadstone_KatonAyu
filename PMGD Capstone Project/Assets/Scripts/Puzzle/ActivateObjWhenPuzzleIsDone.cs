using UnityEngine;

public class ActivateObjWhenPuzzleIsDone : MonoBehaviour
{
    [SerializeField] GameObject[] objToSetActive;

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
            for(int i = 0; i < objToSetActive.Length; i++)
            {
                objToSetActive[i].SetActive(true);
            }
            activateIsDone = true;
        }
    }
}
