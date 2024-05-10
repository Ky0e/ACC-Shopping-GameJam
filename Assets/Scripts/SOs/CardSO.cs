using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eCardType { postCard, idCard, greetingCard, giftCard, creditCard, punchCard, reverseCard, passportCard, timeCard}
[CreateAssetMenu(fileName = "SO", menuName = "Scriptable Objects/Card Object", order = 1)]
public class CardSO : ScriptableObject
{
    public eCardType cardType;
}
