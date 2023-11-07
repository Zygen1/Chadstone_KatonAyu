using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public bool isInteracted;

    public void StopInteract()
    {
        PlayerStats.instance.isPlayerInteract = false;
        isInteracted = false;
    }

    public void InteractObject()
    {
        isInteracted = true;
    }
}


