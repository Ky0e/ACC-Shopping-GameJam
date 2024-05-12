using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum eCardType
{ 
    postCard, idCard, greetingCard, giftCard, creditCard, punchCard, reverseCard, passportCard, timeCard, 
    magicTheGatheringCard, pokemonCard, businessCard, debitCard, heartCard, noteCard, scoreCard, squidGameCard, 
    yugiohCard, keyCard
}
[CreateAssetMenu(fileName = "SO", menuName = "Scriptable Objects/Card Object", order = 1)]
public class CardSO : ScriptableObject
{
    public eCardType cardType;
    public Sprite cardSprite;
    public PlayerProperties modifiedPropertyA;
    public float modifierValueA;
    public PlayerProperties modifiedPropertyB;
    public float modifierValueB;
    public PlayerProperties modifiedPropertyC;
    public float modifierValueC;
    public PlayerProperties modifiedPropertyD;
    public float modifierValueD;
}
