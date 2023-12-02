using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    public static LoadingScreen instance;

    public GameObject loadingPanel;
    public Image loadingBarFill;

    private void Awake()
    {
        instance = this;
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
        Debug.Log("Loading Showed 1");
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        Debug.Log("Loading Showed 2");
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        loadingPanel.SetActive(true);

        while (!operation.isDone)
        {
            Debug.Log("Loading Showed 3");
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBarFill.fillAmount = progressValue;
            yield return null;
        }

        Debug.Log("Loading Showed 4");
    }
}
