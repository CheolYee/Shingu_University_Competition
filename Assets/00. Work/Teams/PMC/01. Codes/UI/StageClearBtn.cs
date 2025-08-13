using UnityEngine;
using UnityEngine.SceneManagement;

namespace _00._Work.Teams.PMC._01._Codes.UI
{
    public class StageClearBtn : MonoBehaviour
    {
        [SerializeField] private bool isTutorial;
        
        public void StageClearBtnClicked()
        {
            if (!SaveManager.IsStageCleared((SceneManager.GetActiveScene().buildIndex + 1).ToString()))
            {
                if (isTutorial)
                    SaveManager.SaveTutorialStageId((SceneManager.GetActiveScene().buildIndex + 1).ToString());
                else
                    SaveManager.SaveStageId((SceneManager.GetActiveScene().buildIndex + 1).ToString());
            }
            
            FadeManager.Instance.FadeToScene(isTutorial ? 1 : 2);
        }
    }
}