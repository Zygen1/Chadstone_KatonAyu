using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorAction { CHANGE_SCENE, ANIMATION, TELEPORT };
public enum LockType { NONE, ITEM, PUZZLE };
public class DoorObject : MonoBehaviour
{
    [Header("Option")]
    [HideInInspector] public LockType lockType;

    [HideInInspector] public string[] itemName;
    private int itemUnlock;
    [HideInInspector] public bool destroyItem;
    [HideInInspector] public DestroyItemInventory destroyItemInventory;

    [HideInInspector] public string puzzleName;

    [Header("Action")]
    [HideInInspector] public DoorAction action;

    [Header("Change Scene")]
    [HideInInspector] public string sceneName;

    [Header("Animation")]
    [HideInInspector] public Animator animator;

    [Header("Teleport")]
    GameObject player;
    [HideInInspector] public GameObject teleportPos;

    InteractableObject interactableObject;
    [SerializeField] bool unlocked;
    bool interactIsDone;
    bool isOpen;

    private void Awake()
    {
        interactableObject = GetComponent<InteractableObject>();
        animator = GetComponent<Animator>();
        if(action == DoorAction.TELEPORT)
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

        if(lockType == LockType.NONE)
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
            else if(lockType == LockType.PUZZLE)
            {
                HandlePuzzleUnlock();
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
            else if (action == DoorAction.ANIMATION)
            {
                AnimationAction();
            }
            if(action == DoorAction.TELEPORT)
            {
                TeleportAction();
            }
        }
    }

    void TeleportAction()
    {
        player.transform.position = teleportPos.transform.position;
        HandleInteraction();
    }

    void ChangeSceneAction()
    {
        GameManager.instance.ChangeScene(sceneName);
        HandleInteraction();
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

        HandleInteraction();
    }

    void HandleInteraction()
    {
        PlayerStats.instance.isPlayerInteract = false;
        interactableObject.isInteracted = false;
    }

    void HandlePuzzleUnlock()
    {
        for (int i = 0; i < PuzzleManager.instance.puzzleStats.Length; i++)
        {
            PuzzleStats puzzleStats = PuzzleManager.instance.puzzleStats[i];
            if (puzzleStats.puzzleName == puzzleName && puzzleStats.isDone)
            {
                unlocked = true;
            }
            else if (puzzleStats.puzzleName == puzzleName && !puzzleStats.isDone)
            {
                Debug.Log("Puzzle is not done");
                HandleInteraction();
            }
        }
    }

    void HandleItemUnlock()
    {
        for(int i = 0; i < itemName.Length; i++)
        {
            if(InventorySystem.instance.SearchItemInInventory(itemName[i]) == true)
            {
                itemUnlock++;
                if (destroyItem)
                {
                    destroyItemInventory.DestroyItem(itemName[i]);
                }

                itemName[i] = null;
            }
        }

        if(itemUnlock == itemName.Length)
        {
            unlocked = true;
        }
        else
        {
            Debug.Log("Item Not Found");
            HandleInteraction();
        }
    }
}
