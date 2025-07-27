using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardNameUI : MonoBehaviour
{
    public GameObject tooltipPanel;
    public TextMeshProUGUI cardNameText;
    public TextMeshProUGUI attributeText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI priceText;

    private void Start()
    {
        tooltipPanel.SetActive(false);
    }

    public void Show(CardData data)
    {
        tooltipPanel.SetActive(true);
        cardNameText.text = data.cardName;
        attributeText.text = data.cardEnum.ToString();
        descriptionText.text = data.explanation;
        priceText.text = $"АЁАн : {data.price}";
    }

    public void Hide()
    {
        tooltipPanel.SetActive(false);
    }
}