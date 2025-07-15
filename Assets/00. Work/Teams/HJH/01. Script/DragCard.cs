using UnityEngine;
using UnityEngine.EventSystems;

public class DragCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public CardData cardData;      // 카드 정보 (ScriptableObject)
    public bool isClone = false;   // 분신 여부

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

        // 드래그 중에는 최상위 캔버스에 배치해서 UI 가림 방지
        transform.SetParent(canvas.transform);
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 마우스 움직임에 따라 카드 이동 (캔버스 스케일 보정)
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        GameObject dropTarget = eventData.pointerEnter;
        CardSlot slot = dropTarget != null ? dropTarget.GetComponent<CardSlot>() : null;

        if (!isClone)
        {
            // 원본 카드일 때
            if (slot != null && !slot.IsOccupied())
            {
                // 슬롯에 분신 카드 생성
                GameObject clone = Instantiate(gameObject, slot.transform);
                RectTransform cloneRect = clone.GetComponent<RectTransform>();
                cloneRect.anchoredPosition = Vector2.zero;

                DragCard cloneDrag = clone.GetComponent<DragCard>();
                cloneDrag.isClone = true;
                cloneDrag.cardData = this.cardData;

                // 슬롯에 속성 효과 적용 호출
                slot.CardEffect(this.cardData.cardEnum);
            }

            // 원본 카드는 무조건 제자리로 돌아감
            transform.SetParent(startParent);
            rectTransform.anchoredPosition = startPosition;
        }
        else
        {
            // 분신 카드일 때
            if (slot != null && !slot.IsOccupied())
            {
                // 슬롯에 배치
                transform.SetParent(slot.transform);
                rectTransform.anchoredPosition = Vector2.zero;

                // 슬롯에 속성 효과 적용 호출
                slot.CardEffect(this.cardData.cardEnum);
            }
            else
            {
                // 슬롯 아닌 곳에 떨어지면 분신 삭제
                Destroy(gameObject);
            }
        }
    }
}