using NaughtyAttributes;
using UnityEngine;

public class OnSignPostInteract : MonoBehaviour, IInteractable
{
    [field: SerializeField] private GameObject canvasPrefab;
    [field: SerializeField] private Transform canvasSpawnPoint;
    [field: SerializeField] private DialogueSet dialogueSet;
    [field: SerializeField, ReadOnly] private Canvas_DialoguePopUp dialogueCanvas;
    private bool isIntertacted;

    public void OnInteract()
    {
        dialogueCanvas.GetNextDialogue();
    }

    public void OnInteractionAreaEntered()
    {
        if (!isIntertacted)
        {
            isIntertacted = true;
            // SPAWN CANVAS AND BILLBOARD
            GameObject _temp = Instantiate(canvasPrefab, canvasSpawnPoint.position, Quaternion.identity, transform);
            if (!_temp.TryGetComponent(out Canvas_DialoguePopUp _dialoguePopUp)) { return; }

            dialogueCanvas = _dialoguePopUp;
            dialogueCanvas.InitializeCanvas(dialogueSet);
        }
    }

    public void OnInteractionAreaExited()
    {
        if (isIntertacted)
        {
            // DESTROY CANVAS
            isIntertacted = false;
            dialogueCanvas.ScaleToZero();
            Destroy(dialogueCanvas.gameObject, 2f);
        }
    }
}
