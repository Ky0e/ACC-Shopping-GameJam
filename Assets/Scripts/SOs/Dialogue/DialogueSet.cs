using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using ProjectStartup.ScriptableObjects.Variables;

[CreateAssetMenu(menuName = "Scriptable Objects/Dialogue/Dialogue Set", fileName = "New Dialogue Set", order = 1)]
public class DialogueSet : RuntimeSet<DialogueStringSet>
{
    [field: SerializeField, Label("Should Follow A Specific Dialogue Path")] private bool followSpecificPath = false;

    [field: SerializeField, ShowIf("followSpecificPath"), Expandable] private List<DialoguePath> Path;
    [field: SerializeField, ReadOnly, BoxGroup("Debug Information"), ShowIf("followSpecificPath")] private int currentDialoguePathSet = 0;
    [field: SerializeField, ReadOnly, BoxGroup("Debug Information"), ShowIf("followSpecificPath")] private int currentDialogueInPath = 0;
    [field: SerializeField, ReadOnly, BoxGroup("Debug Information")] private int currentDialogueSet = 0;
    [field: SerializeField, ReadOnly, BoxGroup("Debug Information")] private int currentDialogue = 0;
    [field: SerializeField, ReadOnly, BoxGroup("Debug Information")] private bool isFirstDialogueOption = true;



    private void OnEnable()
    {
        currentDialogue = 0;
        currentDialogueSet = 0;
        currentDialoguePathSet = 0;
        currentDialogueInPath = 0;
        isFirstDialogueOption = true;
    }
    private void OnDisable()
    {
        currentDialogue = 0;
        currentDialogueSet = 0;
        currentDialoguePathSet = 0;
        currentDialogueInPath = 0;
        isFirstDialogueOption = true;
    }


    public string GetNextDialogue()
    {
        if (followSpecificPath) { return GetSpecificDialoguePath(); }
        if (Items.Count <= 0) { Debug.LogError("PLEASE ADD DIALOGUE TO THE SET!"); return ""; }
        if (currentDialogueSet >= Items.Count) { return ""; }

        if (!isFirstDialogueOption) { currentDialogue++; }
        if (currentDialogue >= Items[currentDialogueSet].Items.Count)
        {
            currentDialogue = 0;
            isFirstDialogueOption = true;
            currentDialogueSet++;
            if (currentDialogueSet >= Items.Count) { return ""; }

        }
        if (isFirstDialogueOption && currentDialogue == 0) { isFirstDialogueOption = false; }

        return Items[currentDialogueSet].Items[currentDialogue].Value;
    }

    private string GetRandomizedDialogue(StringVariableSet _setToRandomizeFrom, int _pathIteratorIndex)
    {
        if (_pathIteratorIndex >= _setToRandomizeFrom.Items.Count) { return ""; }
        string _randomizedDialogue = _setToRandomizeFrom.Items[Random.Range(0, _setToRandomizeFrom.Items.Count)].Value;
        return _randomizedDialogue;
    }


    private string GetSpecificDialoguePath()
    {
        if (Path.Count <= 0) { Debug.LogError("PLEASE ADD DIALOGUE PATHS TO THE PATH!"); return ""; }
        if (currentDialoguePathSet >= Path.Count) { return ""; }
        if (Path[currentDialoguePathSet].useSpecificDialogue)
        {
            string _currentDialogue = Path[currentDialoguePathSet].dialogue.Value;
            currentDialoguePathSet++;
            return _currentDialogue;
        }

        if (Path[currentDialoguePathSet].randomizeDialogue)
        { return GetRandomizedDialogue(Path[currentDialoguePathSet].dialogueSet, currentDialoguePathSet); }

        if (!isFirstDialogueOption) { currentDialogueInPath++; }
        if (currentDialogueInPath >= Items[currentDialoguePathSet].Items.Count)
        {
            currentDialogueInPath = 0;
            isFirstDialogueOption = true;
            currentDialoguePathSet++;
            if (currentDialoguePathSet >= Items.Count) { return ""; }

        }
        if (isFirstDialogueOption && currentDialogueInPath == 0) { isFirstDialogueOption = false; }

        return Items[currentDialoguePathSet].Items[currentDialogueInPath].Value;
    }

    public void ResetDialoguePath()
    {
        currentDialogue = 0;
        currentDialogueSet = 0;
        currentDialoguePathSet = 0;
        currentDialogueInPath = 0;
        isFirstDialogueOption = true;
    }
}
