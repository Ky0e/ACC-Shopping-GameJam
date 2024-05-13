using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random_Card_Picker : MonoBehaviour
{
    [field: SerializeField] CardSO[] availableCards;
    private void Awake()
    {
        GetComponent<InteractablePikup>().keyCard = availableCards[Random.Range(0, availableCards.Length)];
    }
}
