using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFunction_ShowItem : MonoBehaviour
{
    public string interactableObjName;

    public void ShowPainting5()
    {
        UIManager.Instance.ToggleInventoryPanel();
        InteractableObject interactableObject = GameObject.Find(interactableObjName).GetComponent<InteractableObject>();
        PlayerStats.instance.isPlayerInteract = true;
        interactableObject.isInteracted = true;
    }
}
