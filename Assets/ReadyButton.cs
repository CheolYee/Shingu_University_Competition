using _00._Work._02._Scripts.Manager.MoneyManager;
using _00._Work._02._Scripts.Systems;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReadyButton : MonoBehaviour
{
    public Transform[] slotParents;
    public GameObject bulletPrefab;
    public List<CardEnum> cardSequence = new List<CardEnum>();
    public Bullet targetBullet;
    public static List<CardEnum> lastUsedSequence = new List<CardEnum>();
    public OnOffCard onOffCard;
    public MoneyManager moneyManager;
    private void Awake()
    {
        lastUsedSequence.Clear();
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

            targetBullet.Initialize();
        }
        else
        {
            Debug.LogWarning("ReadyButton: Bullet이 등록되지 않았습니다.");
        }

        if (onOffCard != null)
        {
            onOffCard.OpenCards();
        }

        int totalPrice = 0;

        for (int i = 0; i < slotParents.Length; i++)
        {
            Transform slot = slotParents[i];
            if (slot.childCount > 0)
            {
                CardView card = slot.GetChild(0).GetComponent<CardView>();
                if (card != null && card.data != null)
                {
                    string priceStr = card.data.price.Trim();
                    string numericStr = System.Text.RegularExpressions.Regex.Match(priceStr, @"\d+").Value;

                    if (int.TryParse(numericStr, out int price))
                    {
                        totalPrice += price;
                        Debug.Log($"카드 {card.data.cardName} 가격: {price}");
                    }
                    else
                    {
                        Debug.LogWarning($"가격 파싱 실패: 카드 이름 = {card.data.cardName}, 원래 price = \"{priceStr}\"");
                    }
                }
            }
        }

        moneyManager.SpendMoney(totalPrice);
    }

    public void OnClick_UnReady()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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