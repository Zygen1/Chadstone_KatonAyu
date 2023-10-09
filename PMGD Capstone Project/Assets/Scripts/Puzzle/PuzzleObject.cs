using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractableObject))]
public class PuzzleObject : MonoBehaviour
{
    [Header("Option")]
    [HideInInspector] public bool isPuzzleNeedItem;
    [HideInInspector] public string itemName;
    [HideInInspector] public bool destroyItem;
    [HideInInspector] public DestroyItemInventory destroyItemInventory;

    [Header("Requirment")]
    [HideInInspector] public GameObject realPuzzle;
    [HideInInspector] public PuzzleStats puzzleStats;
    
    InteractableObject interactableObject;
    bool itemIsDestroyed;

    private void Awake()
    {
        interactableObject = GetComponent<InteractableObject>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (interactableObject.isInteracted)
        {
            if (!realPuzzle.GetComponent<PuzzleStats>().isDone)
            {
                if (isPuzzleNeedItem)
                {
                    if (InventorySystem.instance.SearchItemInInventory(itemName) == true)
                    {
                        realPuzzle.SetActive(true);
                    }
                    else
                    {
                        interactableObject.isInteracted = false;
                        PlayerStats.instance.isPlayerInteract = false;
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
        else if (!interactableObject.isInteracted)
        {
            realPuzzle.SetActive(false);
        }

        if (destroyItem && !itemIsDestroyed && puzzleStats.isDone)
        {
            Debug.Log("Destroy item");
            destroyItemInventory.DestroyItem(itemName);
            itemIsDestroyed = true;
        }
    }
}
