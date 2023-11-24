using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsRotate : MonoBehaviour
{
    [SerializeField] float rotateValue = 10f;

    [Header("Audio")]
    [SerializeField] AudioSource audioSource;

    PuzzleStats stats;

    private void Start()
    {
        stats = GetComponentInParent<PuzzleStats>();
    }

    private void OnMouseDown()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }

        if (!stats.isDone)
        {
            // Rotasi objek pada sumbu Z sebanyak rotateValue.
            transform.Rotate(0f, 0f, rotateValue);
        }

        Debug.Log("Rotate: " + gameObject.name);
    }
}
