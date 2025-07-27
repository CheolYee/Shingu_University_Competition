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

    private bool isToggled = false;
    private Vector2 aStartPos, bStartPos;

    void Start()
    {
        aStartPos = a.anchoredPosition;
        bStartPos = b.anchoredPosition;

        toggleButton.onClick.AddListener(() =>
        {
            isToggled = !isToggled;

            Vector2 aTarget = isToggled ? aStartPos - new Vector2(0, moveDistance) : aStartPos;
            Vector2 bTarget = isToggled ? bStartPos + new Vector2(0, moveDistance) : bStartPos;

            a.DOAnchorPos(aTarget, moveDuration)
            .SetUpdate(true)
            .OnComplete(() =>
            {
             if (!isToggled)
                 Time.timeScale = 0;
              else
                  Time.timeScale = 1;
            });

            b.DOAnchorPos(bTarget, moveDuration).SetUpdate(true);
        });
    }
}
