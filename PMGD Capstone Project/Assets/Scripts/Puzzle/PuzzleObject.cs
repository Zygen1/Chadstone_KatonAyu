using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractableObject))]
public class PuzzleObject : MonoBehaviour
{
    [Header("Puzzle Need Item")]
    public bool isPuzzleNeedItem;
    public string itemName;
    public bool destroyItem;
    public DestroyItemInventory destroyItemInventory;
    public bool isShowingDialogue;
    public DialogueTrigger dialogueTrigger;

    [Header("Turn Obj To Interactable")]
    public bool isChangeObjToInteractable;
    public GameObject objToChange;
    public bool isDeactiveWhenDone;

    [Header("Requirment")]
    public GameObject realPuzzle;
    public PuzzleStats puzzleStats;

    [Header("Optional")]
    [SerializeField] BoxCollider2D[] disableColidersWhenShowing;

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

        if (disableColidersWhenShowing != null)
        {
            for (int i = 0; i < disableColidersWhenShowing.Length; i++)
            {
                disableColidersWhenShowing[i].enabled = !interactableObject.isInteracted;
            }
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
