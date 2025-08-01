using UnityEngine;
using UnityEngine.EventSystems;

public class CardSlot : MonoBehaviour, IDropHandler
{
    public Bullet bullet;
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount > 0) return;

        GameObject dropped = eventData.pointerDrag;
    }

    public bool IsOccupied()
    {
        return transform.childCount > 0;
    }
    public void CardEffect(CardEnum attribute)
    {
        /*switch (attribute)
        {
            case CardEnum.Bounce:
                // 튕기는 효과 만들면 넣어줘, 밑에는 이름만 써놓을꼐
                bullet.collidier.isTrigger = false;
                break;
            case CardEnum.Through:
                // 관통 효과
                bullet.collidier.isTrigger = true;
                break;
            case CardEnum.SpeedUp:
                bullet.speed = 13f;
                // 속도 증가
                break;
            case CardEnum.Explode:
                // 폭발 이펙트
                break;
            // 등등...
            case CardEnum.Default:
                bullet.speed = 10f; // 기본 속도
                break;
        }*/
    }
}
