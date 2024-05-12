using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Hub", menuName = "Create New Hub")]
public class SoHub : ScriptableObject
{
    [NamedArray(typeof(eCardType))] public CardSO[] soCards;
}
