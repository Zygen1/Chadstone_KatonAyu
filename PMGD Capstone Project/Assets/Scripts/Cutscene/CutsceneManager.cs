using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Teleporting
{
    public GameObject obj;
    public Transform tpPos;
}

public class CutsceneManager : MonoBehaviour
{
    [Header("Atribut")]
    [SerializeField] bool isLiveScene;

    [Header("Is Has Dialogue")]
    [SerializeField] bool isHasDialogue;
    [SerializeField] DialogueTrigger[] dialogueTriggers;
    public bool onDialogueEndRunAfcut;

    [Header("Before Cutscene")]
    public GameObject[] befcutObjToSetActive;
    public GameObject[] befcutObjToSetInactive;
    [Header("After Cutscene")]
    public GameObject[] afcutObjToSetActive;
    public GameObject[] afcutObjToSetInactive;

    [Header("Disable Player Audio Listener")]
    public bool isDisablePlayerAudioListener;
    public AudioListener playerAudioListener;

    [Header("Teleport Obj After Cutscene")]
    [SerializeField] bool isTeleporting;
    [SerializeField] Teleporting[] teleports;
    
    [Header("Requirment")]
    [SerializeField] DialogueManager dialogueManager;

    // Start is called before the first frame update
    void Start()
    {
        if (isDisablePlayerAudioListener)
        {
            playerAudioListener = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioListener>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BeforCutscene()
    {
        if (!isLiveScene)
        {
            PlayerStats.instance.isPlayerInteract = true;
            if (isDisablePlayerAudioListener)
            {
                playerAudioListener.enabled = false;
            }
        }

        if (isHasDialogue)
        {
            /*DialogueTrigger dialogueTrigger = GetComponent<DialogueTrigger>();
            dialogueTrigger.TriggerDialogue();*/
            dialogueManager = GetComponentInParent<DialogueManager>();
        }

        //Looping untuk mengaktifkan obj sebelum cutscene
        for (int i = 0; i < befcutObjToSetActive.Length; i++)
        {
            befcutObjToSetActive[i].SetActive(true);
        }

        //Looping untuk mengaktifkan obj sebelum cutscene
        for (int i = 0; i < befcutObjToSetInactive.Length; i++)
        {
            befcutObjToSetInactive[i].SetActive(false);
        }

        Debug.Log("BEFVCU");
    }

    public void AfterCutscene()
    {
        if (!isLiveScene)
        {
            PlayerStats.instance.isPlayerInteract = false;
            if (isDisablePlayerAudioListener)
            {
                playerAudioListener.enabled = true;
            }
        }

        //Looping untuk mengaktifkan obj setelah cutscene
        for (int i = 0; i < afcutObjToSetActive.Length; i++)
        {
            afcutObjToSetActive[i].SetActive(true);
        }

        //Looping untuk menonaktifkan obj setelah cutscene
        for (int i = 0; i < afcutObjToSetInactive.Length; i++)
        {
            afcutObjToSetInactive[i].SetActive(false);
        }

        if (isTeleporting)
        {
            for(int i = 0; i < teleports.Length; i++)
            {
                teleports[i].obj.transform.position = teleports[i].tpPos.position;
            }
        }
    }

    public void StopState()
    {
        Animator animator = GetComponent<Animator>();
        animator.SetBool("NextState", false);
    }

    public void StopTyping()
    {
        dialogueManager.StopTyping();
    }

    public void SetTypingSpeed(float speed)
    {
        dialogueManager.typingSpeed = speed;
    }

    public void StartDialogue(int dialogueNo)
    {
        dialogueTriggers[dialogueNo].TriggerDialogue();
    }

    public void EndDialogue()
    {
        dialogueManager.EndDialogue();
    }
}
