using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    public float maxHealth;
    public float currentHealth;
    public float maxStamina;
    public float maxFreezing;
    public float freezingRecovery;
    public float currentFreezing;

    public bool isPlayerCanMove;
    public bool isPlayerInteract;
    public bool isPlayerDialogue;
    public bool isPlayerFreezing;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        isPlayerCanMove = true;
        currentHealth = maxHealth;
        currentFreezing = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isPlayerInteract && !isPlayerDialogue)
        {
            isPlayerCanMove = true;
        }
        else
        {
            isPlayerCanMove = false;
        }

        //Handle Freeze
        currentFreezing = Mathf.Clamp(currentFreezing, 0, maxFreezing);
        if(isPlayerFreezing == false)
        {
            currentFreezing -= freezingRecovery * Time.deltaTime;
        }
        
        if(currentFreezing >= 9.9f)
        {
            LevelManager.instance.isGameOver = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            LevelManager.instance.isGameOver = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            LevelManager.instance.isGameOver = true;
        }
    }
}
