using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    public CardData data;

    public Image imageRenderer;
    public Text nameText;
    public Text attributeText;

    void Start()
    {
        if (data != null)
        {
            imageRenderer.sprite = data.cardImage;
            nameText.text = data.cardName;
            attributeText.text = data.cardEnum.ToString();
        }
    }
}