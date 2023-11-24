using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordObject : MonoBehaviour
{
    public int order;
    public bool isMove;

    [Header("Audio")]
    [SerializeField] AudioSource audioSource;

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
        if (audioSource != null)
        {
            audioSource.Play();
        }

        isMove = true;
    }
}
