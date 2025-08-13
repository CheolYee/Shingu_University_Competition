using UnityEngine;
using UnityEngine.SceneManagement;

namespace _00._Work.Teams.PMC._01._Codes.UI
{
    public class StageClearBtn : MonoBehaviour
    {
        [SerializeField] private bool isTutorial;
        
        public void StageClearBtnClicked()
        {
            FadeManager.Instance.FadeToScene(isTutorial ? 1 : 2);
            SaveManager.IsStageCleared(SceneManager.GetActiveScene().buildIndex.ToString());
        }
    }
}