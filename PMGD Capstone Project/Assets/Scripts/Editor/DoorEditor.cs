using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DoorObject))]
public class DoorObjectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DoorObject door = (DoorObject)target;

        // Tampilkan default Inspector
        DrawDefaultInspector();

        // Tambahkan custom GUI di bawah default Inspector
        EditorGUILayout.Space(); // Membuat spasi antara default Inspector dan custom GUI

        EditorGUILayout.LabelField("Custom Settings", EditorStyles.boldLabel);

        door.lockType = (LockType)EditorGUILayout.EnumPopup("Lock Type", door.lockType);

        if (door.lockType == LockType.ITEM)
        {
            EditorGUILayout.LabelField("Item Settings", EditorStyles.boldLabel);
            int itemCount = EditorGUILayout.IntField("Item Count", door.itemName.Length);

            if (itemCount != door.itemName.Length)
            {
                // Inisialisasi ulang array itemName dengan panjang yang baru
                string[] newItemNames = new string[itemCount];
                for (int i = 0; i < Mathf.Min(itemCount, door.itemName.Length); i++)
                {
                    newItemNames[i] = door.itemName[i];
                }
                door.itemName = newItemNames;
            }

            for (int i = 0; i < door.itemName.Length; i++)
            {
                door.itemName[i] = EditorGUILayout.TextField("Item Name " + i, door.itemName[i]);
            }

            door.destroyItem = EditorGUILayout.Toggle("Destroy Item", door.destroyItem);

            if (door.destroyItem)
            {
                door.destroyItemInventory = (DestroyItemInventory)EditorGUILayout.ObjectField(
                    "Destroy Item Inventory", door.destroyItemInventory, typeof(DestroyItemInventory), true);
            }
        }
        else if (door.lockType == LockType.PUZZLE)
        {
            door.puzzleName = EditorGUILayout.TextField("Puzzle Name", door.puzzleName);
        }
        else if (door.lockType == LockType.SWITCH)
        {
            EditorGUILayout.LabelField("Switch Settings", EditorStyles.boldLabel);
            int switchCount = EditorGUILayout.IntField("Switch Count", door.switchList.Length);

            if (switchCount != door.switchList.Length)
            {
                // Inisialisasi ulang array switchList dengan panjang yang baru
                SwitchObject[] newSwitchList = new SwitchObject[switchCount];
                for (int i = 0; i < Mathf.Min(switchCount, door.switchList.Length); i++)
                {
                    newSwitchList[i] = door.switchList[i];
                }
                door.switchList = newSwitchList;
            }

            for (int i = 0; i < door.switchList.Length; i++)
            {
                door.switchList[i] = (SwitchObject)EditorGUILayout.ObjectField("Switch " + i, door.switchList[i], typeof(SwitchObject), true);
            }
        }

        door.action = (DoorAction)EditorGUILayout.EnumPopup("Door Action", door.action);

        if (door.action == DoorAction.CHANGE_SCENE)
        {
            door.sceneName = EditorGUILayout.TextField("Scene Name", door.sceneName);
        }
        else if (door.action == DoorAction.ANIMATION)
        {
            door.animator = (Animator)EditorGUILayout.ObjectField("Animator", door.animator, typeof(Animator), true);
        }
        else if (door.action == DoorAction.TELEPORT)
        {
            door.teleportPos = (GameObject)EditorGUILayout.ObjectField("Teleport Position", door.teleportPos, typeof(GameObject), true);
            door.effectObj = (GameObject)EditorGUILayout.ObjectField("Effect Object", door.effectObj, typeof(GameObject), true);
        }

        door.isUnlockAnim = EditorGUILayout.Toggle("Unlock Animation", door.isUnlockAnim);
        if (door.isUnlockAnim)
        {
            door.unlockAnim = (Animator)EditorGUILayout.ObjectField("Unlock Animator", door.unlockAnim, typeof(Animator), true);
            door.unlockerSprite = (Sprite)EditorGUILayout.ObjectField("Unlocker Sprite", door.unlockerSprite, typeof(Sprite), true);
            door.unlockerRenderer = (SpriteRenderer)EditorGUILayout.ObjectField("Unlocker Renderer", door.unlockerRenderer, typeof(SpriteRenderer), true);
            door.disableObjTime = EditorGUILayout.FloatField("Disable Object Time", door.disableObjTime);
            door.isUnlockDoor = EditorGUILayout.Toggle("Unlock Door", door.isUnlockDoor);
            door.isStopInteract = EditorGUILayout.Toggle("Stop Interact", door.isStopInteract);
            door.lockObj = (GameObject)EditorGUILayout.ObjectField("Lock Object", door.lockObj, typeof(GameObject), true);
        }

        door.showDialogOnLock = EditorGUILayout.Toggle("Show Dialog On Lock", door.showDialogOnLock);

        if (door.showDialogOnLock)
        {
            door.dialogueTrigger = (DialogueTrigger)EditorGUILayout.ObjectField("Dialogue Trigger", door.dialogueTrigger, typeof(DialogueTrigger), true);
        }
    }
}
