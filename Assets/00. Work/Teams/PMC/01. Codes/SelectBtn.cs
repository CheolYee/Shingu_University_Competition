using _00._Work.Teams.PMC._01._Codes.UI;
using _00.Work.Scripts.UI;
using TMPro;
using UnityEngine;

namespace _00._Work.Teams.PMC._01._Codes
{
    public class SelectBtn : MonoBehaviour
    {
        [SerializeField] private GameObject lockPanel;
        [SerializeField] private TextMeshProUGUI stageText;
        [SerializeField] private string id;
        [SerializeField] private bool isTutorial;

        private void Start()
        {
            if (SaveManager.IsStageCleared(id, isTutorial))
            {
                lockPanel.SetActive(false);
            }
        }

        public void RoadStage()
        {
            FadeManager.Instance.FadeToScene(int.Parse(id));
        }
    }
}
