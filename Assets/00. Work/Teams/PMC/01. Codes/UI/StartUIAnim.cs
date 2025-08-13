using System.Collections.Generic;
using _00.Work.Scripts.Managers;
using _00.Work.Scripts.UI;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _00._Work.Teams.PMC._01._Codes.UI
{
    public class StartUIAnim : MonoBehaviour
    {
        public List<Button> uiElements = new List<Button>();
        public float offSet;
        public float duration;
        public float interval;
        
        private List<Vector2> startPositions = new List<Vector2>();

        private void Start()
        {
            foreach (var btn in uiElements)
            {
                btn.interactable = false;
            }
            
            for (int i = 0; i < uiElements.Count; i++)
            {
                var btn = uiElements[i];
                var content = uiElements[i].GetComponent<RectTransform>();
                startPositions.Add(content.anchoredPosition);
                content.anchoredPosition += Vector2.left * offSet;
                
                content.DOAnchorPos(startPositions[i], duration).
                    SetEase(Ease.OutCubic).
                    SetDelay(i * interval).
                    OnComplete(() => btn.interactable = true);
            }
        }

        public void StartBtnClicked()
        {
            SoundManager.Instance.PlaySfx("buttonPress");
            FadeManager.Instance.FadeToScene(2);
        }
        
        public void TutorialBtnClicked()
        {
            SoundManager.Instance.PlaySfx("buttonPress");
            FadeManager.Instance.FadeToScene(1);
        }

        public void ExitBtnClicked()
        {
            SoundManager.Instance.PlaySfx("buttonPress");
            Application.Quit();
        }
    }
}
