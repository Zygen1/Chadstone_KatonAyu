using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MbahGAnim : MonoBehaviour
{
    private SpriteRenderer targetSprite;
    private EnemyAI enemyAI;
    private Animator animator;
    private float horizontalPosition;
    private float verticalPosition;
    public GameObject target;

    void Start()
    {
        targetSprite = GetComponentInParent<SpriteRenderer>();
        enemyAI = GetComponentInParent<EnemyAI>();
        animator = GetComponentInParent<Animator>();
    }

    void FixedUpdate()
    {
        CheckDirection();
    }

    private void Update()
    {
        
    }

    private void CheckDirection()
    {
        float xPosition =  target.transform.position.x - gameObject.transform.position.x ;
        float yPosition = target.transform.position.y - gameObject.transform.position.y;
        //Debug.Log("X = " + xPosition + "Y = " + yPosition); 
        animator.SetFloat("Horizontal", Mathf.Clamp(xPosition,-1,1));
        animator.SetFloat("Vertical", Mathf.Clamp(yPosition, -1, 1));
        
    }

   
}
