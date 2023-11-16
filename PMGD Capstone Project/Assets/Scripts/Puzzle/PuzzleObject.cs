using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractableObject))]
public class PuzzleObject : MonoBehaviour
{
    [Header("Puzzle Need Item")]
    [HideInInspector] public bool isPuzzleNeedItem;
    [HideInInspector] public string itemName;
    [HideInInspector] public bool destroyItem;
    [HideInInspector] public DestroyItemInventory destroyItemInventory;
    [HideInInspector] public bool isShowingDialogue;
    [HideInInspector] public DialogueTrigger dialogueTrigger;

    [Header("Turn Obj To Interactable")]
    [HideInInspector] public bool isChangeObjToInteractable;
    [HideInInspector] public GameObject objToChange;
    [HideInInspector] public bool isDeactiveWhenDone;

    [Header("Requirment")]
    [HideInInspector] public GameObject realPuzzle;
    [HideInInspector] public PuzzleStats puzzleStats;
    
    InteractableObject interactableObject;
    bool itemIsDestroyed;

    private void Awake()
    {
        interactableObject = GetComponent<InteractableObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (interactableObject.isInteracted)
        {
            HandlePuzzleInteract();
        }
        else if (!interactableObject.isInteracted)
        {
            HandlePuzzleIgnore();
        }

        if (destroyItem && !itemIsDestroyed && puzzleStats.isDone)
        {
            destroyItemInventory.DestroyItem(itemName);
            itemIsDestroyed = true;
        }
    }

    void HandlePuzzleInteract()
    {
        if (!realPuzzle.GetComponent<PuzzleStats>().isDone)
        {
            if (isPuzzleNeedItem)
            {
                if (InventorySystem.instance.SearchItemInInventory(itemName) == true)
                {
                    if (isShowingDialogue)
                    {
                        dialogueTrigger.enabled = false;
                    }

                    realPuzzle.SetActive(true);
                }
                else
                {
                    //NOTIFIKASI ///////////////////////////////////////////////////////
                    Debug.Log("Need : " + itemName + " item");
                    //END NOTIFIKASI ///////////////////////////////////////////////////
                    if (!isShowingDialogue)
                    {
                        interactableObject.StopInteract();
                    }
                }
            }
            else if (!isPuzzleNeedItem)
            {
                realPuzzle.SetActive(true);
            }
        }
        else
        {
            realPuzzle.SetActive(true);
        }
    }

    void HandlePuzzleIgnore()
    {
        if (puzzleStats.isDone)
        {
            HandlePuzzleComplete();
        }

        realPuzzle.SetActive(false);
    }

    void HandlePuzzleComplete()
    {
        if (isDeactiveWhenDone)
        {
            interactableObject.StopInteract();
            gameObject.SetActive(false);
        }

        if (isChangeObjToInteractable)
        {
            objToChange.tag = "InteractableObject";
        }

        if (isShowingDialogue)
        {
            dialogueTrigger.enabled = false;
        }
    }
}
