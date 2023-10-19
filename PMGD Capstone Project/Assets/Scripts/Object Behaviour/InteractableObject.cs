using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public bool isInteracted;

    public void HandleInteraction()
    {
        PlayerStats.instance.isPlayerInteract = false;
        isInteracted = false;
    }
}


