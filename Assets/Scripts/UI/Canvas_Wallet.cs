using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas_Wallet : MonoBehaviour
{
    [field: SerializeField] private GameObject walletClosed;
    [field: SerializeField] private GameObject walletOpened;
    [field: SerializeField] private InputReader manager_Input;
    [field: SerializeField] List<GameObject> cardObjects;
    [field: SerializeField] Image currentCard;
    private bool isOpen = false;


    private void Start()
    {
        if(Player_Inventory.Instance != null)
        {
            Player_Inventory.Instance.OnItemsListUpdated += DetermineCardActive;
        }
        //walletOpened.SetActive(false);
        //walletClosed.SetActive(true);
    }

    private void Update()
    {
        if(manager_Input == null) { return; }
        if (isOpen && !manager_Input.ToggleWallet)
        {
            isOpen = false;
            walletClosed.SetActive(true);
            walletOpened.SetActive(false);
        }
        else if (!isOpen && manager_Input.ToggleWallet)
        {
            isOpen = true;
            walletClosed.SetActive(false);
            walletOpened.SetActive(true);
        }
    }

    private void DetermineCardActive()
    {
        Debug.Log("Card Picked Up");
        foreach (GameObject p in cardObjects)
        {
            card temp = p.GetComponent<card>();
            //Debug.Log(temp.ToString());
            if (p.activeInHierarchy && !Player_Inventory.Instance.HasCardOfType(temp.cardSO.cardType))
            {
                // Disable Card If The Player Doesn't Have
                p.SetActive(false);
            }
            else if (!p.activeInHierarchy && Player_Inventory.Instance.HasCardOfType(temp.cardSO.cardType))
            {
                p.SetActive(true);
            }
        }
    }

    public void SetCurrentCard(eCardType _typeToSetTo)
    {
        Sprite _spriteToSetTo = cardObjects[(int)_typeToSetTo].GetComponent<Image>().sprite;
        if (_spriteToSetTo == currentCard.sprite) { return; }
        currentCard.sprite = _spriteToSetTo;
        currentCard.rectTransform.localPosition = new(currentCard.rectTransform.localPosition.x, 180f, currentCard.rectTransform.localPosition.z);
        currentCard.rectTransform.localScale = Vector3.zero;

        LeanTween.scale(currentCard.gameObject, Vector3.one, .5f).setEaseInBack().setEaseOutQuad();
        LeanTween.moveLocalY(currentCard.gameObject, 210f, .25f).setLoopPingPong(1).setEaseInBack().setEaseOutQuad();
        LeanTween.moveLocalY(currentCard.gameObject, 0f, .25f).setDelay(.5f).setEaseInBack().setEaseOutQuad();
    }
}
