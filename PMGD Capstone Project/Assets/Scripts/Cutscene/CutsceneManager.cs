using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    [Header("Atribut")]
    [SerializeField] bool isLiveScene;

    [Header("Is Has Dialogue")]
    [SerializeField] bool isHasDialogue;
    public bool onDialogueEndRunAfcut;

    [Header("Before Cutscene")]
    public GameObject[] befcutObjToSetActive;
    public GameObject[] befcutObjToSetInactive;
    [Header("After Cutscene")]
    public GameObject[] afcutObjToSetActive;
    public GameObject[] afcutObjToSetInactive;

    [Header("Requirment")]
    public AudioListener playerAudioListener;
    DialogueManager dialogueManager;

    // Start is called before the first frame update
    void Start()
    {
        playerAudioListener = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioListener>();
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
            playerAudioListener.enabled = false;
        }

        if (isHasDialogue)
        {
            DialogueTrigger dialogueTrigger = GetComponent<DialogueTrigger>();
            dialogueTrigger.TriggerDialogue();
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
    }

    public void AfterCutscene()
    {
        if (!isLiveScene)
        {
            PlayerStats.instance.isPlayerInteract = false;
            playerAudioListener.enabled = true;
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
    }

    public void StopState()
    {
        Animator animator = GetComponent<Animator>();
        animator.SetBool("NextState", false);
    }

    public void SetTypingSpeed(float speed)
    {
        dialogueManager.typingSpeed = speed;
    }
}
