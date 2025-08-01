using System.Collections.Generic;
using UnityEngine;

public class ReadyButton : MonoBehaviour
{
    public Transform[] slotParents;
    public GameObject bulletPrefab;
    public List<CardEnum> cardSequence = new List<CardEnum>();
    public Bullet targetBullet;
    public static List<CardEnum> lastUsedSequence = new List<CardEnum>();

    public void RegisterBullet(Bullet bullet)
    {
        targetBullet = bullet;
    }

    public void OnClick_Ready()
    {
        cardSequence = GetCardSequenceFromSlots();
        lastUsedSequence = new List<CardEnum>(cardSequence);
        Debug.Log($"ReadyButton 시퀀스 저장: {string.Join(", ", cardSequence)}");
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
                    Debug.Log($"슬롯 {i}: {card.data.cardEnum}");
                }
                else
                {
                    Debug.Log($"슬롯 {i}: 카드 정보 없음");
                }
            }
            else
            {
                Debug.Log($"슬롯 {i}: 비어 있음");
            }
        }

        return sequence;
    }
}