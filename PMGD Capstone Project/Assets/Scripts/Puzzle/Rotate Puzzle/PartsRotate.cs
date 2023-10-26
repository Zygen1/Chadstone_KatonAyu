using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsRotate : MonoBehaviour
{
    [SerializeField] float rotateValue = 10f;

    PuzzleStats stats;

    private void Start()
    {
        stats = GetComponentInParent<PuzzleStats>();
    }

    private void OnMouseDown()
    {
        if (!stats.isDone)
        {
            // Rotasi objek pada sumbu Z sebanyak rotateValue.
            transform.Rotate(0f, 0f, rotateValue);
        }

        Debug.Log("Rotate: " + gameObject.name);
    }
}
