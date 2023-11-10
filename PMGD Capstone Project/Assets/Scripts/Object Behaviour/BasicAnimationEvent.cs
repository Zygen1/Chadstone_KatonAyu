using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicAnimationEvent : MonoBehaviour
{
    public void DisableObject()
    {
        gameObject.SetActive(false);
    }

    public void ChangeScene(string sceneName)
    {
        if(loadingScreen.instance != null)
        {
            loadingScreen.instance.LoadScene(sceneName);
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
