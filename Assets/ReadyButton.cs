using System.Collections.Generic;
using UnityEngine;

public class ReadyButton : MonoBehaviour
{
    public Transform[] slotParents;
    public GameObject bulletPrefab;
    public List<CardEnum> cardSequence = new List<CardEnum>();
    public Bullet targetBullet;
    public static List<CardEnum> lastUsedSequence = new List<CardEnum>();

    private void Awake()
    {
        lastUsedSequence.Clear(); // 게임 시작 시 초기화
    }

    public void RegisterBullet(Bullet bullet)
    {
        targetBullet = bullet;
    }

    public void OnClick_Ready()
    {
        cardSequence = GetCardSequenceFromSlots();
        lastUsedSequence = new List<CardEnum>(cardSequence);
        Debug.Log($"ReadyButton 시퀀스 저장: {string.Join(", ", cardSequence)}");

        if (targetBullet != null)
        {
            targetBullet.SetCardSequence(cardSequence);
            Debug.Log("비활성화 여부: " + !targetBullet.gameObject.activeInHierarchy);

            // 🔹 Bullet이 꺼져있어도 Initialize 강제 호출
            targetBullet.Initialize();
        }
        else
        {
            Debug.LogWarning("ReadyButton: Bullet이 등록되지 않았습니다.");
        }
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