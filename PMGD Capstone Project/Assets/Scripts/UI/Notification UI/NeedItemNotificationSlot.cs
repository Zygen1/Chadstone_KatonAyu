using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NeedItemNotificationSlot : MonoBehaviour
{

    [Header("Atribut")]
    public TextMeshProUGUI label;
    private string firstText = "You Need ";
    private string secondText = " To Perform This Action!!!";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClosePopUp()
    {
        NotificationUI.instance.CloseNotification();
    }

    public void PauseGame() {
    Time.timeScale = 0.0f;
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }

    public void Set(string item)
    {
        label.text = firstText + item + secondText;
    }
}
