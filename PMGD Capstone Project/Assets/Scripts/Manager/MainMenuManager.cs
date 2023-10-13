using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Instruksi Panel")]
    [SerializeField] GameObject howToPlayPanel;
    [SerializeField] GameObject instruksiKey1;
    [SerializeField] GameObject instruksiKey2;

    [Header("Pengaturan Panel")]
    [SerializeField] GameObject settingPanel;

    [Header("Pencapaian Panel")]
    [SerializeField] GameObject pencapaianPanel;
    [SerializeField] GameObject fullStoryPanel;
    [SerializeField] GameObject[] endingList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame()
    {
        Debug.Log("Clicked");
        SceneManager.LoadScene("Prototype");
        SoundManager.instance.UIClickSfx();
    }

    public void ResumeGame()
    {
        SceneManager.LoadScene("Prototype");
        SoundManager.instance.UIClickSfx();
    }

    public void Manual()
    {
        howToPlayPanel.SetActive(true);
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
            SoundManager.instance.UIClickSfx();
        }
        else
        {
            instruksiKey1.SetActive(false);
            instruksiKey2.SetActive(true);
            SoundManager.instance.UIClickSfx();
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
}
