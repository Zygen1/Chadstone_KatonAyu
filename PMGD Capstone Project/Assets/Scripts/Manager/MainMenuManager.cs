using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] AudioClip startingBGM;
    [SerializeField] Material freezingMat;

    [Header("Instruksi Panel")]
    [SerializeField] GameObject howToPlayPanel;
    [SerializeField] GameObject instruksiKey1OffBtn;
    [SerializeField] GameObject instruksiKey1OnBtn;
    [SerializeField] GameObject instruksiKey2OffBtn;
    [SerializeField] GameObject instruksiKey2OnBtn;
    [SerializeField] GameObject instruksiKey1;
    [SerializeField] GameObject instruksiKey2;

    [Header("Pengaturan Panel")]
    [SerializeField] GameObject settingPanel;

    [Header("Pencapaian Panel")]
    [SerializeField] GameObject pencapaianPanel;
    [SerializeField] GameObject fullStoryPanel;
    [SerializeField] GameObject[] endingList;

    [Header("Slider For Sound Effect")]
    [SerializeField] Slider BGMSlider;
    [SerializeField] Slider SFXSlider;

    // Start is called before the first frame update
    void Start()
    {
        freezingMat.SetFloat("_FullscreenIntensity", 0);
        SoundManager.instance.changeMusic(startingBGM);
        DataManager.instance.LoadData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScene(string scene_name)
    {
        Debug.Log("Clicked");
        loadingScreen.instance.LoadScene(scene_name);
        SoundManager.instance.UIClickSfx();
    }

    public void ResumeGame(string scene_name)
    {
        SceneManager.LoadScene(scene_name);
        SoundManager.instance.UIClickSfx();
    }

    public void Manual()
    {
        howToPlayPanel.SetActive(true);
        if (PlayerPrefs.HasKey("ActiveController"))
        {
            if (PlayerPrefs.GetFloat("ActiveController") == 1)
            {
                instruksiKey1.SetActive(true);
                instruksiKey2.SetActive(false);
                instruksiKey1OnBtn.SetActive(true);
                instruksiKey1OffBtn.SetActive(false);
                instruksiKey2OffBtn.SetActive(true);
                instruksiKey2OnBtn.SetActive(false);
                instruksiKey1OnBtn.GetComponent<Button>().interactable = false;
            }
            else
            {
                instruksiKey1.SetActive(false);
                instruksiKey2.SetActive(true);
                instruksiKey1OnBtn.SetActive(false);
                instruksiKey1OffBtn.SetActive(true);
                instruksiKey2OffBtn.SetActive(false);
                instruksiKey2OnBtn.SetActive(true);
                instruksiKey2OnBtn.GetComponent<Button>().interactable = false;
            }
        }
        else
        {
            ChangeKeyButton(1);
        }
        SoundManager.instance.UIClickSfx();
    }

    public void Achievments()
    {
        pencapaianPanel.SetActive(true);
        SoundManager.instance.UIClickSfx();
    }

    public void Setting()
    {
        settingPanel.SetActive(true);
        SoundManager.instance.UIClickSfx();
        BGMSlider.value = PlayerPrefs.GetFloat("BGMVolume");
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
    }

    public void ExitGame()
    {
        Application.Quit();
        SoundManager.instance.UIClickSfx();
    }

    public void ChangeKeyButton(int key)
    {
        if (key == 1)
        {
            instruksiKey1.SetActive(true);
            instruksiKey2.SetActive(false);
            instruksiKey1OnBtn.SetActive(true);
            instruksiKey1OffBtn.SetActive(false);
            instruksiKey2OffBtn.SetActive(true);
            instruksiKey2OnBtn.SetActive(false);
            instruksiKey1OnBtn.GetComponent<Button>().interactable = false;
            PlayerPrefs.SetFloat("ActiveController", key);
            SoundManager.instance.UIClickSfx();
        }
        else
        {
            instruksiKey1.SetActive(false);
            instruksiKey2.SetActive(true);
            instruksiKey1OnBtn.SetActive(false);
            instruksiKey1OffBtn.SetActive(true);
            instruksiKey2OffBtn.SetActive(false);
            instruksiKey2OnBtn.SetActive(true);
            instruksiKey2OnBtn.GetComponent<Button>().interactable = false;
            SoundManager.instance.UIClickSfx();
            PlayerPrefs.SetFloat("ActiveController", key);
        }
    }

    public void CheckFullStory(GameObject ending)
    {
        fullStoryPanel.SetActive(true);
        foreach (GameObject go in endingList)
        {
            if (ending == go)
            {
                go.SetActive(true);
            }
        }
        SoundManager.instance.UIClickSfx();

    }

    public void BackToAchievments()
    {
        fullStoryPanel.SetActive(false);
        foreach (GameObject go in endingList)
        {
            go.SetActive(false);
        }
        SoundManager.instance.UIClickSfx();
    }

    public void BackToMenu()
    {
        howToPlayPanel.SetActive(false);
        settingPanel.SetActive(false);
        pencapaianPanel.SetActive(false);
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
