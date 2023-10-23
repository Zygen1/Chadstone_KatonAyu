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

    [Header("UI")]
    [SerializeField] GameObject[] inactiveUIObjects;
    /*[SerializeField] GameObject UIControl;
    [SerializeField] GameObject UIStats;*/

    [SerializeField] private Queue<string> sentences;
    Dialogue currentDialogue;
    DialogueTrigger currentDialogueTrigger;
    Animator currentCutsceneAnimator;
    CutsceneManager currentCutsceneManager;

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
        /*UIControl.SetActive(false);
        UIStats.SetActive(false);*/
        for(int i = 0; i < inactiveUIObjects.Length; i++)
        {
            inactiveUIObjects[i].SetActive(false);
        }

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
        /*UIControl.SetActive(false);
        UIStats.SetActive(false);*/
        for (int i = 0; i < inactiveUIObjects.Length; i++)
        {
            inactiveUIObjects[i].SetActive(false);
        }
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
        if(sentences.Count == 0)
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
            dialogue.text += letter;
            yield return null;
        }
    }

    private void EndDialogue()
    {
        if(currentDialogue != null && currentDialogueTrigger != null)
        {
            if (currentDialogue.afterDialogue == AfterDialogue.ACTIVATE_OBJ)
            {
                currentDialogue.objToActivate.SetActive(true);

                if (currentDialogue.nextDialogueAfterActivate)
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
        }

        if (isDialogueCutscene)
        {
            currentCutsceneManager.AfterCutscene();
        }

        //Setup
        animator.SetBool("IsOpen", false);
        PlayerStats.instance.isPlayerDialogue = false;
        PlayerStats.instance.isPlayerInteract = false;
        /*UIControl.SetActive(true);
        UIStats.SetActive(true);*/
        for (int i = 0; i < inactiveUIObjects.Length; i++)
        {
            inactiveUIObjects[i].SetActive(true);
        }
    }
}
