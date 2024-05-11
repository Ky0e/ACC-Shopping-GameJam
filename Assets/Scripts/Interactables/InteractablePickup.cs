using NaughtyAttributes;
using ProjectStartup.ScriptableObjects.Variables;
using UnityEngine;

public class InteractablePikup : MonoBehaviour, IInteractable
{
    [field: SerializeField] ScriptableObject keyCard;


    public void OnInteract()
    {

    }

    public void OnInteractionAreaEntered()
    {
        //Debug.Log("YOU EARNED COINS");
        //coinFloatVariable.Value += Random.Range(coinAmountRange.x, coinAmountRange.y);
        //transform.GetComponent<Collider>().enabled = false;
        //transform.GetComponent<Renderer>().enabled = false;
        Destroy(gameObject, .5f);
    }

    public void OnInteractionAreaExited()
    {
    }
}