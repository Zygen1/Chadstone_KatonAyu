using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] Animator inventoryPanel;
    [SerializeField] bool isInventoryOpen;
    [SerializeField] bool toggleButtonCheck;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        KeyboardControl();
    }

    [ContextMenu("Toggle Inventory")]
    public void ToggleInventoryPanel()
    {
        isInventoryOpen = !isInventoryOpen;
        inventoryPanel.SetBool("Open", isInventoryOpen);
    }

    void KeyboardControl()
    {
        if(PlayerStats.instance.isPlayerInteract == false)
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
}
