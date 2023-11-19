using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject pausePanel;
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
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        pausePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        SoundManager.instance.UIClickSfx();
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0.0f;
        BGMSlider.value = PlayerPrefs.GetFloat("BGMVolume");
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        SoundManager.instance.UIClickSfx();
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1.0f;
        SoundManager.instance.UIClickSfx();
    }

    public void ChangeBGMVolume()
    {
        float volume = BGMSlider.value;
        SoundManager.instance.ChangeBGMVolumeFromAnotherScript(volume);
    }

    public void ChangeSFXVolume()
    {
        float volume = SFXSlider.value;
        SoundManager.instance.ChangeSFXVolumeFromAnotherScript(volume);
    }
}
