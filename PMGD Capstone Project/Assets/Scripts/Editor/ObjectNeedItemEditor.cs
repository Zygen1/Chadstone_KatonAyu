using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ObjectNeedItem))]
public class ObjectNeedItemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ObjectNeedItem objectNeedItem = (ObjectNeedItem)target;

        DrawDefaultInspector();

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Custom Settings", EditorStyles.boldLabel);

        // Display and edit the itemName array
        int itemArraySize = EditorGUILayout.IntField("Item Name Array Size", objectNeedItem.itemName.Length);

        // Ensure the itemArraySize is not negative
        if (itemArraySize < 0)
        {
            itemArraySize = 0;
        }

        // Resize the itemName array if the size changed
        if (itemArraySize != objectNeedItem.itemName.Length)
        {
            string[] newItemNameArray = new string[itemArraySize];
            for (int i = 0; i < Mathf.Min(itemArraySize, objectNeedItem.itemName.Length); i++)
            {
                newItemNameArray[i] = objectNeedItem.itemName[i];
            }
            objectNeedItem.itemName = newItemNameArray;
        }

        for (int i = 0; i < objectNeedItem.itemName.Length; i++)
        {
            objectNeedItem.itemName[i] = EditorGUILayout.TextField("Item Name " + i, objectNeedItem.itemName[i]);
        }

        objectNeedItem.destroyItem = EditorGUILayout.Toggle("Destroy Item", objectNeedItem.destroyItem);

        EditorGUILayout.LabelField("Switch Object", EditorStyles.boldLabel);
        objectNeedItem.isASwitchObj = EditorGUILayout.Toggle("Is a Switch Object", objectNeedItem.isASwitchObj);
        if (objectNeedItem.isASwitchObj)
        {
            objectNeedItem.switchObject = (SwitchObject)EditorGUILayout.ObjectField("Switch Object", objectNeedItem.switchObject, typeof(SwitchObject), true);
        }

        EditorGUILayout.LabelField("Change Dialogue", EditorStyles.boldLabel);
        objectNeedItem.isChangeDialogue = EditorGUILayout.Toggle("Change Dialogue", objectNeedItem.isChangeDialogue);
        if (objectNeedItem.isChangeDialogue)
        {
            objectNeedItem.isParentDialogue = EditorGUILayout.Toggle("Is Parent Dialogue", objectNeedItem.isParentDialogue);
            objectNeedItem.otherDialogueTrig = (DialogueTrigger)EditorGUILayout.ObjectField("Dialogue Trigger", objectNeedItem.otherDialogueTrig, typeof(DialogueTrigger), true);
            objectNeedItem.forceStart = EditorGUILayout.Toggle("Force Start Dialogue", objectNeedItem.forceStart);
        }

        EditorGUILayout.LabelField("Activate Object", EditorStyles.boldLabel);
        objectNeedItem.isActivateObj = EditorGUILayout.Toggle("Activate Object", objectNeedItem.isActivateObj);
        if (objectNeedItem.isActivateObj)
        {
            objectNeedItem.objToActivate = (GameObject)EditorGUILayout.ObjectField("Object to Activate", objectNeedItem.objToActivate, typeof(GameObject), true);
        }

        EditorGUILayout.LabelField("Give Item", EditorStyles.boldLabel);
        objectNeedItem.isGiveItem = EditorGUILayout.Toggle("Give Item", objectNeedItem.isGiveItem);
        if (objectNeedItem.isGiveItem)
        {
            objectNeedItem.objGiveItem = (ObjectGiveItem)EditorGUILayout.ObjectField("Object Give Item", objectNeedItem.objGiveItem, typeof(ObjectGiveItem), true);
        }

        EditorGUILayout.LabelField("Requirement", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("Status", EditorStyles.boldLabel);
    }
}
