using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class loadingScreen : MonoBehaviour
{
    public static loadingScreen instance;

    public GameObject loadingPanel;
    public Image loadingBarFill;

    private void Awake()
    {
        instance = this;
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        loadingPanel.SetActive(true);

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBarFill.fillAmount = progressValue;
            yield return null;
        }
    }
}
