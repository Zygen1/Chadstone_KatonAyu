using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleObor : MonoBehaviour
{
    public bool isOnFire = true;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick() 
    {
        isOnFire = !isOnFire;
        animator.SetBool("isOnFire", isOnFire);
    }
}
