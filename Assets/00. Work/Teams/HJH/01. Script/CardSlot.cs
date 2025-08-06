using _00._Work._02._Scripts.Systems;
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
                // ƨ��� ȿ�� ����� �־���, �ؿ��� �̸��� �������
                bulletCollid.collid.isTrigger = false;
                break;
            case CardEnum.Penetration:
                // ���� ȿ��
                bulletCollid.collid.isTrigger = true;
                break;
            case CardEnum.SpeedUp:
                bulletCollid.speed = 13f;
                // �ӵ� ����
                break;
            case CardEnum.Explode:
                // ���� ����Ʈ
                break;
            // ���...
            case CardEnum.Default:
                bulletCollid.speed = 10f; // �⺻ �ӵ�
                break;
        }*/
    }
}
