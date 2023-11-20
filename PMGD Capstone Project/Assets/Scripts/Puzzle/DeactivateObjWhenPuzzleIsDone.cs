using UnityEngine;

public class DeactivateObjWhenPuzzleIsDone : MonoBehaviour
{
    [SerializeField] GameObject[] objToSetDeactive;

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
            for (int i = 0; i < objToSetDeactive.Length; i++)
            {
                objToSetDeactive[i].SetActive(false);
            }
            activateIsDone = true;
        }
    }
}
