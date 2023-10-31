using UnityEngine;

public enum AfterDialogue { NONE, ACTIVATE_OBJ, NEED_ITEM}

[System.Serializable]
public class Dialogue
{
    public string name;
    [TextArea(3, 10)]
    public string[] sentences;
    public AfterDialogue afterDialogue;

    [Tooltip("Force start next dialogue 'AfterDialogue' ")] 
    public bool isForceStart;
    public bool nextDialogue;

    [Header("After Dialogue Activate Obj")]
    public GameObject objToActivate;

    [Header("After Dialogue Need Item")]
    public string itemName;
}
