using System.Collections.Generic;
using UnityEngine;

public class ReadyButton : MonoBehaviour
{
    public Transform[] slotParents;

    public List<CardEnum> cardList = new List<CardEnum>();

    public void OnClick_Ready()
    {
        cardList = GetCardSequenceFromSlots();
    }

    private List<CardEnum> GetCardSequenceFromSlots()
    {
        List<CardEnum> sequence = new List<CardEnum>();

        for (int i = 0; i < slotParents.Length; i++)
        {
            Transform slot = slotParents[i];
            if (slot.childCount > 0)
            {
                CardView card = slot.GetChild(0).GetComponent<CardView>();
                if (card != null && card.data != null)
                {
                    sequence.Add(card.data.cardEnum);
                }
            }
        }
        return sequence;
    }
}