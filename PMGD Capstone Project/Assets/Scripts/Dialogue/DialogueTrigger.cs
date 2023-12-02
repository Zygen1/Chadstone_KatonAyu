using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TriggerMethod { TRIGGER, DIALOGUE_INTERACT}
//[RequireComponent(typeof(DialogueTrigger))]
public class DialogueTrigger : MonoBehaviour
{
    public bool isCutsceneDialogue;

    public TriggerMethod triggerMethod;
    public int currentDialogue;
    public Dialogue[] dialogue;
    public bool checkFirst;

    [Header("Requirment")]
    //[SerializeField] DialoguableObject dialogueObject;
    [SerializeField] InteractableObject interactableObject;
    //[SerializeField] DialogueHandler dialogueHandler;

    private void Start()
    {
        //dialogueHandler = GetComponent<DialogueHandler>();

        if (triggerMethod == TriggerMethod.DIALOGUE_INTERACT)
        {
            //dialogueObject = gameObject.GetComponent<DialoguableObject>();
            interactableObject = gameObject.GetComponent<InteractableObject>();
        }
    }

    private void Update()
    {
        if (triggerMethod == TriggerMethod.DIALOGUE_INTERACT)
        {
            /*if (dialogueObject.isThereDialogue)
            {
                TriggerDialogue();
                dialogueObject.isThereDialogue = false;
                *//*PlayerStats.instance.isPlayerDialogue = false;*//*
            }*/

            if (!checkFirst)
            {
                if (interactableObject.isInteracted)
                {
                    TriggerDialogue();
                    interactableObject.isInteracted = false;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TriggerDialogue();
        }
    }

    [ContextMenu("Trigger Dialogue")]
    public void TriggerDialogue()
    {
        //FindObjectOfType<DialogueManager>().StartDialogue(dialogue[dialogueHandler.currentDialogue]);
        if (isCutsceneDialogue)
        {
            DialogueManager cutsceneDialogueManager = GetComponentInParent<DialogueManager>();
            if(cutsceneDialogueManager != null)
            {
                Debug.Log("Daptet Boy");
            }
            cutsceneDialogueManager.StartCutsceneDialogue(dialogue[currentDialogue], this, GetComponent<Animator>(), GetComponent<CutsceneManager>());
        }
        else
        {
            //FindObjectOfType<DialogueManager>().StartDialogue(dialogue[currentDialogue], this);
            DialogueManager dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
            dialogueManager.StartDialogue(dialogue[currentDialogue], this);
        }
        //Debug.Log("Dialogue Is Started");
    }
}
