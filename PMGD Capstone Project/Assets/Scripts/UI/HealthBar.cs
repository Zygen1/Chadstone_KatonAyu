using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] PlayerStats playerStats;
    [SerializeField] Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        float healthPercentage = playerStats.currentHealth / playerStats.maxHealth;
        slider.value = healthPercentage;
    }
}
