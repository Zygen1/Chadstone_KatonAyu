using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorAction { CHANGE_SCENE, ANIMATION, TELEPORT };
public enum LockType { NONE, ITEM, PUZZLE, SWITCH };
public class DoorObject : MonoBehaviour
{
    [Header("Option")]
    [HideInInspector] public LockType lockType;

    //Locktype Item
    [HideInInspector] public string[] itemName;
    private int itemUnlock;
    [HideInInspector] public bool destroyItem;
    [HideInInspector] public DestroyItemInventory destroyItemInventory;

    //Locktype Puzzle
    [HideInInspector] public string puzzleName;

    //Locktype Switch
    [HideInInspector] public SwitchObject[] switchList;
    private int switchUnlock;

    [Header("Action")]
    [HideInInspector] public DoorAction action;

    [Header("Change Scene")]
    [HideInInspector] public string sceneName;

    [Header("Animation")]
    [HideInInspector] public Animator animator;

    [Header("Teleport")]
    GameObject player;
    [HideInInspector] public GameObject teleportPos;

    [Header("Option")]
    [Tooltip("Set Inactive Object when unlocked")] public bool disableOnUnlock;
    public GameObject objToSetInactive;

    [Header("Debug")]
    [SerializeField] bool unlocked;

    InteractableObject interactableObject;
    bool interactIsDone;
    bool isOpen;

    private void Awake()
    {
        interactableObject = GetComponent<InteractableObject>();
        animator = GetComponent<Animator>();
        if (action == DoorAction.TELEPORT)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (destroyItem)
        {
            destroyItemInventory = GetComponent<DestroyItemInventory>();
        }

        if (lockType == LockType.NONE)
        {
            unlocked = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (interactableObject.isInteracted)
        {
            //Lock type item
            if (lockType == LockType.ITEM)
            {
                HandleItemUnlock();
            }
            //Lock type puzzle
            if (lockType == LockType.PUZZLE)
            {
                HandlePuzzleUnlock();
            }
            //Lock type switch
            if(lockType == LockType.SWITCH)
            {
                HandleSwitchUnlock();
            }

            PerformAction();
        }
    }

    void PerformAction()
    {
        if (unlocked)
        {
            if (action == DoorAction.CHANGE_SCENE)
            {
                ChangeSceneAction();
            }
            
            if (action == DoorAction.ANIMATION)
            {
                AnimationAction();
            }
            
            if (action == DoorAction.TELEPORT)
            {
                TeleportAction();
            }

        }
    }

    void TeleportAction()
    {
        player.transform.position = teleportPos.transform.position;
        //HandleInteraction();
        interactableObject.StopInteract();
    }

    void ChangeSceneAction()
    {
        GameManager.instance.ChangeScene(sceneName);
        //HandleInteraction();
        interactableObject.StopInteract();
    }

    void AnimationAction()
    {
        isOpen = !isOpen;
        if (isOpen)
        {
            animator.SetBool("Open", true);
        }
        else
        {
            animator.SetBool("Open", false);
        }

        //HandleInteraction();
        interactableObject.StopInteract();
    }

    IEnumerator DisableObjectAction()
    {
        //HandleInteraction();
        interactableObject.StopInteract();

        // Dapatkan komponen SpriteRenderer dari objek yang akan dinonaktifkan
        SpriteRenderer spriteRenderer = objToSetInactive.GetComponent<SpriteRenderer>();

        // Inisialisasi alpha menjadi 1 (tidak transparan)
        float alpha = 1f;

        // Lintasan berkurangnya alpha per detik
        float alphaReductionRate = 0.5f; // Sesuaikan dengan kecepatan yang Anda inginkan

        // Loop sampai alpha mencapai 0
        while (alpha > 0)
        {
            // Kurangi alpha
            alpha -= alphaReductionRate * Time.deltaTime;

            // Atur alpha ke SpriteRenderer
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);

            // Tunggu frame selanjutnya
            yield return null;
        }

        // Setelah alpha mencapai 0, nonaktifkan objek
        objToSetInactive.SetActive(false);
        unlocked = true;
    }


    /*void HandleInteraction()
    {
        PlayerStats.instance.isPlayerInteract = false;
        interactableObject.isInteracted = false;
    }*/

    void HandlePuzzleUnlock()
    {
        for (int i = 0; i < PuzzleManager.instance.puzzleStats.Length; i++)
        {
            PuzzleStats puzzleStats = PuzzleManager.instance.puzzleStats[i];
            if (puzzleStats.puzzleName == puzzleName && puzzleStats.isDone)
            {

                if (disableOnUnlock)
                {
                    StartCoroutine(DisableObjectAction());
                }
                else
                {
                    unlocked = true;
                }
            }
            else if (puzzleStats.puzzleName == puzzleName && !puzzleStats.isDone)
            {
                Debug.Log("Puzzle is not done");
                //HandleInteraction();
                interactableObject.StopInteract();
            }
        }
    }

    void HandleItemUnlock()
    {
        for (int i = 0; i < itemName.Length; i++)
        {
            if (InventorySystem.instance.SearchItemInInventory(itemName[i]) == true)
            {
                itemUnlock++;
                if (destroyItem)
                {
                    destroyItemInventory.DestroyItem(itemName[i]);
                }

                itemName[i] = null;
            }
        }

        if (itemUnlock == itemName.Length)
        {
            if (disableOnUnlock)
            {
                StartCoroutine(DisableObjectAction());
            }
            else
            {
                unlocked = true;
            }
        }
        else
        {
            Debug.Log("Item Not Found");
            //HandleInteraction();
            interactableObject.StopInteract();
        }
    }

    void HandleSwitchUnlock()
    {
        for(int i = 0; i < switchList.Length; i++)
        {
            if (switchList[i].isOn)
            {
                switchUnlock++;
            }
            else
            {
                switchUnlock = 0;
                unlocked = false;
                interactableObject.StopInteract();
                return;
            }
        }

        if(switchUnlock == switchList.Length)
        {
            if (disableOnUnlock)
            {
                StartCoroutine(DisableObjectAction());
            }
            else
            {
                unlocked = true;
            }
        }
    }
}
