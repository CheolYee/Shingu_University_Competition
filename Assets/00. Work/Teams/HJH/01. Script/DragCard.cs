using UnityEngine;
using UnityEngine.EventSystems;

public class DragCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public CardData cardData;
    public bool isClone = false;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas canvas;

    private Transform startParent;
    private Vector2 startPosition;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startParent = transform.parent;
        startPosition = rectTransform.anchoredPosition;

        transform.SetParent(canvas.transform);
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        GameObject dropTarget = eventData.pointerEnter;
        CardSlot slot = dropTarget != null ? dropTarget.GetComponent<CardSlot>() : null;

        if (!isClone)
        {
            if (slot != null && !slot.IsOccupied())
            {
                GameObject clone = Instantiate(gameObject, slot.transform);
                RectTransform cloneRect = clone.GetComponent<RectTransform>();
                cloneRect.anchoredPosition = Vector2.zero;

                DragCard cloneDrag = clone.GetComponent<DragCard>();
                cloneDrag.isClone = true;
                cloneDrag.cardData = this.cardData;

                slot.CardEffect(this.cardData.cardEnum);
            }

            transform.SetParent(startParent);
            rectTransform.anchoredPosition = startPosition;
        }
        else
        {
            if (slot != null && !slot.IsOccupied())
            {
                transform.SetParent(slot.transform);
                rectTransform.anchoredPosition = Vector2.zero;

                slot.CardEffect(this.cardData.cardEnum);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}