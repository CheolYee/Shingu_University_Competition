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

        Time.timeScale = 0;
        isToggled = false;

        toggleButton.onClick.AddListener(() =>
        {
            isToggled = !isToggled;

            Vector2 aTarget = isToggled ? aStartPos - new Vector2(0, moveDistance) : aStartPos;
            Vector2 bTarget = isToggled ? bStartPos + new Vector2(0, moveDistance) : bStartPos;

            Time.timeScale = isToggled ? 1 : 0;

            a.DOAnchorPos(aTarget, moveDuration).SetUpdate(true);
            b.DOAnchorPos(bTarget, moveDuration).SetUpdate(true);
        });


    }
    public void OpenCards()
    {
        if (isToggled) return;

        isToggled = true;

        Vector2 aTarget = aStartPos - new Vector2(0, moveDistance);
        Vector2 bTarget = bStartPos + new Vector2(0, moveDistance);

        Time.timeScale = 1;

        a.DOAnchorPos(aTarget, moveDuration).SetUpdate(true);
        b.DOAnchorPos(bTarget, moveDuration).SetUpdate(true);
    }
}
