using UnityEngine;
using UnityEngine.EventSystems;

public class CardSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        // 이미 자식이 있으면 받지 않음
        if (transform.childCount > 0) return;

        GameObject dropped = eventData.pointerDrag;
    }

    public bool IsOccupied()
    {
        return transform.childCount > 0;
    }
    public void CardEffect(CardEnum attribute)
    {
        switch (attribute)
        {
            case CardEnum.Bounce:
                // 튕기는 효과 만들면 넣어줘, 밑에는 이름만 써놓을꼐
                break;
            case CardEnum.Through:
                // 관통 효과
                break;
            case CardEnum.SpeedUp:
                // 속도 증가
                break;
            case CardEnum.Explode:
                // 폭발 이펙트
                break;
                // 등등...
        }
    }
}
