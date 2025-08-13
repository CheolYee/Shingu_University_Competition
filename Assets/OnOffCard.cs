using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class OnOffCard : MonoBehaviour
{
    public Button toggleButton;
    public RectTransform a;
    public RectTransform b;
    public float moveDistance = 100f;
    public float moveDuration = 0.3f;

    public bool isToggled = false;
    private Vector2 aStartPos, bStartPos;

    void Start()
    {
        aStartPos = a.anchoredPosition;
        bStartPos = b.anchoredPosition;

        // 처음부터 열려 있는 상태로 만들기
        Vector2 aTarget = aStartPos - new Vector2(0, moveDistance);
        Vector2 bTarget = bStartPos + new Vector2(0, moveDistance);

        a.anchoredPosition = aTarget;
        b.anchoredPosition = bTarget;

        Time.timeScale = 1;
        isToggled = false;

        toggleButton.onClick.AddListener(() =>
        {
            isToggled = !isToggled;

            Vector2 newATarget = isToggled ? aStartPos : aStartPos - new Vector2(0, moveDistance);
            Vector2 newBTarget = isToggled ? bStartPos : bStartPos + new Vector2(0, moveDistance);

            Time.timeScale = isToggled ? 0 : 1;

            a.DOAnchorPos(newATarget, moveDuration).SetUpdate(true);
            b.DOAnchorPos(newBTarget, moveDuration).SetUpdate(true);
        });
    }

    public void OpenCards()
    {
        if (!isToggled) return;

        isToggled = false;

        Vector2 aTarget = aStartPos - new Vector2(0, moveDistance);
        Vector2 bTarget = bStartPos + new Vector2(0, moveDistance);

        Time.timeScale = 1;

        a.DOAnchorPos(aTarget, moveDuration).SetUpdate(true);
        b.DOAnchorPos(bTarget, moveDuration).SetUpdate(true);
    }
}