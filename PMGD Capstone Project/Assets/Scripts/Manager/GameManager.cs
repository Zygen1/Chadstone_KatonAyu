using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] AudioClip startingBGM;
    public GameObject pausePanel;
    [SerializeField] Slider BGMSlider;
    [SerializeField] Slider SFXSlider;

    [Header("Debug")]
    [SerializeField] bool pauseInput;
    public bool isCutsceneActive;

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
        SoundManager.instance.changeMusic(startingBGM);
        pausePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCutsceneActive && !PlayerStats.instance.isPlayerInteract && !PlayerStats.instance.isPlayerDialogue)
        {
            float pauseInputValue = InputManager.inputSystem.UI.Pause.ReadValue<float>();
            if (pauseInputValue > 0 && !pauseInput)
            {
                PauseGame();
                pauseInput = true;
            }
            else if (pauseInputValue == 0)
            {
                pauseInput = false;
            }
        }

    }

    public void ChangeScene(string sceneName)
    {
        Time.timeScale = 1;
        /*SceneManager.LoadScene(sceneName);*/
        LoadingScreen.instance.LoadScene(sceneName);
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

    public void RestartGame()
    {
        DataManager.instance.ResetData();
    }

    public void GoodEndingIsUnlock()
    {
        DataManager.instance.goodEndingUnlock = true;
        DataManager.instance.SaveData();
    }

    public void SecretEndingIsUnlock()
    {
        DataManager.instance.secretEndingUnlock = true;
        DataManager.instance.SaveData();
    }
}
