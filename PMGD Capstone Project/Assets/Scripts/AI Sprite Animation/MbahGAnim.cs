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

    private void CheckDirection()
    {
        horizontalPosition = enemyAI.GetDirection().x;
        verticalPosition = enemyAI.GetDirection().y;

        animator.SetFloat("Horizontal", horizontalPosition);
        animator.SetFloat("Vertical", verticalPosition);
        
    }

   
}
