using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordObject : MonoBehaviour
{
    public int order;
    public bool isMove;

    [Header("Audio")]
    [SerializeField] AudioSource audioSource;

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
            if (audioSource != null)
            {
                audioSource.Play();
            }

            isMove = true;
        }
    }
}
