using UnityEngine;
using UnityEngine.EventSystems;

public class CardView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public CardData data;

    public CardNameUI cardNameUI;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (cardNameUI != null && data != null)
            cardNameUI.Show(data);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (cardNameUI != null)
            cardNameUI.Hide();
    }
}
