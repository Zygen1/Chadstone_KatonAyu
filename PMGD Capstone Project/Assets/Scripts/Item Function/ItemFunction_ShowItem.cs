using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFunction_ShowItem : MonoBehaviour
{
    //public string interactableObjName;

    public void ShowItem(string objName)
    {
        UIManager.Instance.ToggleInventoryPanel();
        InteractableObject interactableObject = GameObject.Find(objName).GetComponent<InteractableObject>();
        PlayerStats.instance.isPlayerInteract = true;
        interactableObject.isInteracted = true;
    }
}
