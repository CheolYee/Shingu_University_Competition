using System;
using System.Collections;
using _00.Work.Scripts.Managers;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _00._Work.Teams.PMC._01._Codes.UI
{
    public class FadeManager : MonoSingleton<FadeManager>
    {
        [Header("Fade UI")]
        public CanvasGroup fadeGroup;
        public float fadeDuration = 1f;

        protected override void Awake()
        {
            base.Awake();
            if (Instance == this)
            {
                DontDestroyOnLoad(gameObject);
                FadeOut();
            }
        }

        public void FadeOut(Action onComplete = null)
        {
            if (fadeGroup == null)
            {
                return;
            }
            
            fadeGroup.gameObject.SetActive(true);
            fadeGroup.alpha = 1;
            fadeGroup.DOFade(0f, fadeDuration).SetUpdate(true).OnComplete(() =>
            {
                fadeGroup.gameObject.SetActive(false);
                onComplete?.Invoke();
            });
        }

        private void FadeIn(Action onFadeComplete = null)
        {
            if (fadeGroup == null)
            {
                return;
            }
            fadeGroup.gameObject.SetActive(true);
            fadeGroup.alpha = 0;
            fadeGroup.DOFade(1f, fadeDuration).SetUpdate(true).OnComplete(() =>
            {
                onFadeComplete?.Invoke();
                FadeOut();
            });
        }
        
        public void FadeToScene(int sceneIndex)
        {
            FadeIn(() =>
            {
                SceneManager.LoadScene(sceneIndex);
            });

            if (SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().buildIndex == 1)
            {
                SoundManager.Instance.PlayBgm("MenuBGM");
            }

        }


        public void FadeToSceneDelay(int sceneIndex)
        {
            StartCoroutine(DelayAndFadeToScene(sceneIndex));
        }

        private IEnumerator DelayAndFadeToScene(int sceneIndex)
        {
            yield return null; // 한 프레임 대기: 모든 Awake() 보장
            FadeToScene(sceneIndex);
        }
    }
}