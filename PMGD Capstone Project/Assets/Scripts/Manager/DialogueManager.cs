using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    public bool isDialogueCutscene;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogue;
    public Animator animator;
    public float typingSpeed = 0.05f;

    [SerializeField] private Queue<string> sentences;
    Dialogue currentDialogue;
    DialogueTrigger currentDialogueTrigger;
    Animator currentCutsceneAnimator;
    CutsceneManager currentCutsceneManager;

    [Header("Audio")]
    [SerializeField] AudioSource audioSource;

    bool nextDialogueInput;
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    private void Update()
    {
        float nextDialogueInputValue = InputManager.inputSystem.Player.NextDialogue.ReadValue<float>();
        if(nextDialogueInputValue > 0 && !nextDialogueInput)
        {
            nextDialogueInput = true;
            DisplayNextSentence();
        }
        else if(nextDialogueInputValue == 0)
        {
            nextDialogueInput = false;
        }
    }

    public void StartDialogue(Dialogue dialogue, DialogueTrigger dialogueTrigger)
    {

        //Setup
        currentDialogue = dialogue;
        currentDialogueTrigger = dialogueTrigger;
        PlayerStats.instance.isPlayerDialogue = true;

        animator.SetBool("IsOpen", true);

        nameText.text = currentDialogue.name;

        sentences.Clear();

        foreach(string sentence in currentDialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void StartCutsceneDialogue(Dialogue dialogue, DialogueTrigger dialogueTrigger, Animator cutsceneAnimator, CutsceneManager cutsceneManager)
    {

        //Setup
        currentDialogue = dialogue;
        currentDialogueTrigger = dialogueTrigger;
        PlayerStats.instance.isPlayerDialogue = true;
        currentCutsceneAnimator = cutsceneAnimator;
        currentCutsceneManager = cutsceneManager;

        animator.SetBool("IsOpen", true);

        nameText.text = currentDialogue.name;

        sentences.Clear();

        foreach (string sentence in currentDialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    [ContextMenu("Display Next Dialogue")]
    public void DisplayNextSentence()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
            Debug.Log("AUDIO PLAY");
        }

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        if(isDialogueCutscene)
        {
            currentCutsceneAnimator.SetBool("NextState", true);
            Debug.Log("lanjut state");
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogue.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            if(audioSource != null)
            {
                audioSource.Play();
                Debug.Log("AUDIO PLAY");
            }
            else
            {
                Debug.Log("No Audio");
            }
            dialogue.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    private void EndDialogue()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
            Debug.Log("AUDIO PLAY");
        }

        if (currentDialogue != null && currentDialogueTrigger != null)
        {
            if (currentDialogue.afterDialogue == AfterDialogue.ACTIVATE_OBJ)
            {
                currentDialogue.objToActivate.SetActive(true);

                if (currentDialogue.nextDialogue)
                {
                    currentDialogueTrigger.currentDialogue++;
                }

                if (currentDialogue.isForceStart)
                {
                    currentDialogueTrigger.TriggerDialogue();
                    return;
                }
            }
            else if (currentDialogue.afterDialogue == AfterDialogue.NEED_ITEM)
            {
                if (InventorySystem.instance.SearchItemInInventory(currentDialogue.itemName) == true)
                {
                    InventoryItem invetoryItem = InventorySystem.instance.GetReferenceItemDataInInventory(currentDialogue.itemName);
                    InventorySystem.instance.Remove(invetoryItem.data.referenceData);
                    currentDialogueTrigger.currentDialogue++;

                    if (currentDialogue.isForceStart)
                    {
                        currentDialogueTrigger.TriggerDialogue();
                        return;
                    }
                }
            }
            else if(currentDialogue.afterDialogue == AfterDialogue.NONE)
            {
                if (currentDialogue.nextDialogue)
                {
                    currentDialogueTrigger.currentDialogue++;
                }

                if (currentDialogue.isForceStart)
                {
                    currentDialogueTrigger.TriggerDialogue();
                    return;
                }
            }
        }

        if (isDialogueCutscene)
        {
            if (currentCutsceneManager.onDialogueEndRunAfcut)
            {
                currentCutsceneManager.AfterCutscene();
            }
            else
            {
                currentCutsceneAnimator.SetBool("NextState", true);
            }
        }

        //Setup
        animator.SetBool("IsOpen", false);
        PlayerStats.instance.isPlayerDialogue = false;
        PlayerStats.instance.isPlayerInteract = false;
    }
}
