using _00._Work._02._Scripts.Manager.MoneyManager;
using TMPro;
using UnityEngine;

namespace _00._Work._02._Scripts.UI
{
    public class GoldUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI moneyText;

        private void Start()
        {
            moneyText.text = ($"{MoneyManager.Instance.Money}");

            MoneyManager.Instance.OnMoneyChanged += UpdateUI;
        }

        private void UpdateUI()
        {
            moneyText.text = ($"{MoneyManager.Instance.Money}");
        }

        private void OnDestroy()
        {
            if (MoneyManager.Instance != null)
                MoneyManager.Instance.OnMoneyChanged -= UpdateUI;
        }
    }
}
