using NaughtyAttributes;
using ProjectStartup.ScriptableObjects.Variables;
using UnityEngine;

public class InteractablePikup : MonoBehaviour, IInteractable
{
    public CardSO keyCard;
    [field: SerializeField] private GameObject canvasPrefab;
    [field: SerializeField] private Transform canvasSpawnPoint;
    [field: SerializeField, ReadOnly] private Canvas_DialoguePopUp interactCanvas;

    private bool isIntertacted;

    public void OnInteract()
    {
        // SPAWN CANVAS AND BILLBOARd
        Debug.Log("YOU COLLECTED KEY");
        //coinFloatVariable.Value += Random.Range(coinAmountRange.x, coinAmountRange.y);
        //Player_Inventory inventory = GetComponent<Player_Inventory>();
        transform.GetComponent<Collider>().enabled = false;
        transform.GetComponent<Renderer>().enabled = false;
        this.GetComponent<MeshRenderer>().enabled = false;
        Player_Inventory.Instance.AddItem(keyCard);
        Destroy(gameObject, 3f);

        // DESTROY CANVAS
        isIntertacted = false;
        interactCanvas.ScaleToZero();
        Destroy(interactCanvas.gameObject, 2f);
    }

    public void OnInteractionAreaEntered()
    {
        if (!isIntertacted)
        {
            isIntertacted = true;
            // SPAWN CANVAS AND BILLBOARD
            GameObject _temp = Instantiate(canvasPrefab, canvasSpawnPoint.position, Quaternion.identity, transform);
            if (!_temp.TryGetComponent(out Canvas_DialoguePopUp _dialoguePopUp)) { return; }
            interactCanvas = _dialoguePopUp;
        }
    }

    public void OnInteractionAreaExited()
    {
        if (isIntertacted)
        {
            // DESTROY CANVAS
            isIntertacted = false;
            interactCanvas.ScaleToZero();
            Destroy(interactCanvas.gameObject, 2f);
        }
    }
}