using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Dialogue/Types/Dialogue Path", fileName = "New Dialogue Set", order = 0)]
public class DialoguePath : ScriptableObject
{
    [field: SerializeField] private string pathName;
    [field: SerializeField, OnValueChanged("OnUseDialogueSetUpdated"), BoxGroup("Dialogue Option Properties")] public bool useDialogueSet = true;
    [field: SerializeField, AllowNesting, OnValueChanged("OnUseSpecificDialogueUpdated"), BoxGroup("Dialogue Option Properties")] public bool useSpecificDialogue = false;


    [Tooltip("Gets A Random Dialogue Choice From The Dialogue Set."), BoxGroup("Dialogue Set Properties")]
    [field: SerializeField, ShowIf("useDialogueSet"), Label("Should Randomize Dialogue Chosen From Set")] public bool randomizeDialogue;
    [field: SerializeField, AllowNesting, HideIf("useSpecificDialogue"), BoxGroup("Dialogue Set Properties"), Expandable] public StringVariableSet dialogueSet;

    [field: SerializeField, AllowNesting, HideIf("useDialogueSet"), BoxGroup("Dialogue String Properties"), Expandable] public StringVariable dialogue;


    private void OnUseSpecificDialogueUpdated()
    {
        if (useSpecificDialogue) { useDialogueSet = false; }
        else { useDialogueSet = true; }
    }
    private void OnUseDialogueSetUpdated()
    {
        if (useDialogueSet) { useSpecificDialogue = false; }
        else { useSpecificDialogue = true; }
    }
}