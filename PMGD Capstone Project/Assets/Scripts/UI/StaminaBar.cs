using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    [SerializeField] PlayerStats playerStats;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        float staminaPercentage = playerMovement.currentStamina / playerStats.maxStamina;
        slider.value = staminaPercentage;
    }
}
