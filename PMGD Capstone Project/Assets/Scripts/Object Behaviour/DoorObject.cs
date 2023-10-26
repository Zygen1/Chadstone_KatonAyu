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
            if (lockType == LockType.SWITCH)
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
        interactableObject.StopInteract();
    }

    void ChangeSceneAction()
    {
        GameManager.instance.ChangeScene(sceneName);
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

        interactableObject.StopInteract();
    }

    IEnumerator DisableObjectAction()
    {
        interactableObject.StopInteract();

        SpriteRenderer spriteRenderer = objToSetInactive.GetComponent<SpriteRenderer>();

        float alpha = 1f;
        float alphaReductionRate = 0.5f;

        // Loop sampai alpha mencapai 0
        while (alpha > 0)
        {
            alpha -= alphaReductionRate * Time.deltaTime;

            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);

            yield return null;
        }

        // Setelah alpha mencapai 0, nonaktifkan objek
        objToSetInactive.SetActive(false);
        unlocked = true;
    }

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
                //NOTIFIKASI ///////////////////////////////////////////////////////
                Debug.Log("Locked");
                //END NOTIFIKASI ///////////////////////////////////////////////////

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
            //NOTIFIKASI ///////////////////////////////////////////////////////
            Debug.Log("Locked");
            //END NOTIFIKASI ///////////////////////////////////////////////////


            interactableObject.StopInteract();
        }
    }

    void HandleSwitchUnlock()
    {
        for (int i = 0; i < switchList.Length; i++)
        {
            if (switchList[i].isOn)
            {
                switchUnlock++;
            }
            else
            {
                //NOTIFIKASI ///////////////////////////////////////////////////////
                Debug.Log("Locked");
                //END NOTIFIKASI ///////////////////////////////////////////////////

                switchUnlock = 0;
                unlocked = false;
                interactableObject.StopInteract();
                return;
            }
        }

        if (switchUnlock == switchList.Length)
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
