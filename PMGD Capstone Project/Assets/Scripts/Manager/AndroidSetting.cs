using UnityEngine;

public class AndroidSetting : MonoBehaviour
{
    private void Awake()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;
    }
}
