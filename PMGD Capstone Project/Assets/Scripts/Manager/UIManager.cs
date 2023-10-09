using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] Animator inventoryPanel;
    [SerializeField] bool isInventoryOpen;
    [SerializeField] bool toggleButtonCheck;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        KeyboardControl();
    }

    public void ToggleInventoryPanel()
    {
        isInventoryOpen = !isInventoryOpen;
        inventoryPanel.SetBool("Open", isInventoryOpen);
    }

    void KeyboardControl()
    {
        float toggleInventoryInput = InputManager.inputSystem.UI.ToggleInventory.ReadValue<float>();

        if (toggleInventoryInput > 0 && !toggleButtonCheck)
        {
            ToggleInventoryPanel();
            toggleButtonCheck = true;
        }
        else if (toggleInventoryInput <= 0)
        {
            toggleButtonCheck = false;
        }
    }
}
