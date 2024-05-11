using TMPro;
using UnityEngine;

public class Canvas_DialoguePopUp : MonoBehaviour
{
    [field: SerializeField] private TMP_Text dialogueText;
    [field: SerializeField] private GameObject parentCanvas;
    [field: SerializeField] private GameObject dialoguePanel;
    [field: SerializeField] private bool shouldResetDialogue = true;
    private DialogueSet dialogueSet;

    public void InitializeCanvas(DialogueSet _dialogueSet)
    {
        if (shouldResetDialogue) { _dialogueSet.ResetDialoguePath(); }
        dialoguePanel.transform.localScale = Vector3.zero;
        dialogueSet = _dialogueSet;
        GetNextDialogue();
        LeanTween.scale(dialoguePanel, Vector3.one, .25f);
    }

    public void GetNextDialogue()
    {
        dialogueText.SetText(dialogueSet.GetNextDialogue());

        if (dialogueText.text == "") 
        {
            ScaleToZero();
            Destroy(parentCanvas,2f);
        }
    }

    public void ScaleToZero() => LeanTween.scale(dialoguePanel, Vector3.zero, .1f);
}
