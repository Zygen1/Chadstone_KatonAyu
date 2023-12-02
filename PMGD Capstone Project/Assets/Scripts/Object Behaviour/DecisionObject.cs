using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionObject : MonoBehaviour
{
    public GameObject[] deactiveObjWhenDecision;
    public GameObject[] activateObjWhenDecision;
    bool isActive;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive)
        {
            DialogueManager.instance.isCanContinueDialogue = false;
            for(int i = 0; i < deactiveObjWhenDecision.Length; i++)
            {
                deactiveObjWhenDecision[i].SetActive(false);
            }

            PlayerStats.instance.isPlayerDialogue = true;
            PlayerStats.instance.isPlayerInteract = true;
            isActive = true;
        }
    }

    public void ActivateObjectButton(GameObject obj)
    {
        obj.SetActive(true);
    }

    public void DeactiveObjectButton(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void EnableDialogueButton(DialogueTrigger dt)
    {
        dt.enabled = true;
    }

    public void DisableDialogueButton(DialogueTrigger dt)
    {
        dt.enabled = false;
    }

    public void TriggerObjectButton(InteractableObject io)
    {
        io.isInteracted = true;
    }

    public void CanContinueDialogue()
    {
        DialogueManager.instance.isCanContinueDialogue = true;
    }

    public void PlayerStopInteract()
    {
        for(int i = 0; i < activateObjWhenDecision.Length; i++)
        {
            activateObjWhenDecision[i].SetActive(true);
        }

        PlayerStats.instance.isPlayerDialogue = false;
        PlayerStats.instance.isPlayerInteract = false;
    }
}
