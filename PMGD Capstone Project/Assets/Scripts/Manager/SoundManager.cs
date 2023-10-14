using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] AudioMixer audioMixer;

    [SerializeField] AudioSource BGM;
    [SerializeField] AudioSource SFX;

    [SerializeField] AudioClip uiButton;

    [SerializeField] Slider BGMSlider;
    [SerializeField] Slider SFXSlider;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("BGMVolume"))
        {
            LoadBGMVolume();
        }
        else
        {
            ChangeBGMVolume();
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            LoadSFXVolume();
        }
        else
        {
            ChangeSFXVolume();
        }
    }

    public void ChangeBGMVolume()
    {
        float volume = BGMSlider.value;
        audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("BGMVolume", volume);
    }

    private void LoadBGMVolume()
    {
        BGMSlider.value = PlayerPrefs.GetFloat("BGMVolume");
        ChangeBGMVolume();
    }

    public void ChangeSFXVolume()
    {
        float volume = SFXSlider.value;
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    private void LoadSFXVolume()
    {
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        ChangeSFXVolume();
    }

    public void UIClickSfx()
    {
        SFX.PlayOneShot(uiButton);
    }
}
