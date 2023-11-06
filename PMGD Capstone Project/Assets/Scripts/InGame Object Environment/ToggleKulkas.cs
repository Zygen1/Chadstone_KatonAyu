using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleKulkas : MonoBehaviour
{
    Animator animator;
    private bool isBuka = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClick() 
    {
        isBuka = !isBuka;
        animator.SetBool("isBuka", isBuka);
    }
}
