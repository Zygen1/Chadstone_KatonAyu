using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connector : MonoBehaviour
{
    PuzzleStats stats;

    private void Awake()
    {
        stats = GetComponentInParent<PuzzleStats>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (!stats.isDone)
        {
            ConnectPowerPuzzle.instance.connected = true;
            ConnectPowerPuzzle.instance.deactivateObject.SetActive(false);
            ConnectPowerPuzzle.instance.activateObject.SetActive(true);
            transform.Rotate(0f, 0f, 90f);
        }
    }
}
