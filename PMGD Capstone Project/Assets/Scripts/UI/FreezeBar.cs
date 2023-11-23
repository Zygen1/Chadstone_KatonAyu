using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FreezeBar : MonoBehaviour
{
    [SerializeField] PlayerStats playerStats;
    [SerializeField] Slider slider;
    [SerializeField] Material freezinMaterial;

    [Header("Debug")]
    [SerializeField] float freezePrecentage;
    Color matColor;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        slider = GetComponent<Slider>();
        matColor = freezinMaterial.GetColor("_BaseColor");
    }

    // Update is called once per frame
    void Update()
    {

        freezePrecentage = playerStats.currentFreezing / playerStats.maxFreezing;
        freezePrecentage = Mathf.Clamp01(freezePrecentage);

        slider.value = freezePrecentage;
        freezinMaterial.SetFloat("_FullscreenIntensity", freezePrecentage);
    }
}
