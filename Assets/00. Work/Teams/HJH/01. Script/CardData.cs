using UnityEngine;

[CreateAssetMenu(fileName = "NewCardData", menuName = "Card/CardData")]
public class CardData : ScriptableObject
{
    public string cardName;
    public Sprite cardImage;
    public CardEnum cardEnum;
    public string description;
}