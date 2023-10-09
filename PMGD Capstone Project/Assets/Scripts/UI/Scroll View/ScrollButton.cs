using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isDown = false;

    /*// Update is called once per frame
    void Update()
    {
        float scrollDownBtnValue = InputManager.inputSystem.UI.ScrollDown.ReadValue<float>();
        float scrollUpBtnValue = InputManager.inputSystem.UI.ScrollUp.ReadValue<float>();

        if(scrollDownBtnValue > 0 || scrollUpBtnValue > 0)
        {
            isDown = true;
        }
        else
        {
            isDown = false;
        }
    }
*/
    public void OnPointerDown(PointerEventData eventData)
    {
        isDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDown = false;
    }
    
}
