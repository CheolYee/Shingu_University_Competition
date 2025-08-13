using System;
using _00._Work.Teams.PMC._01._Codes;
using _00.Work.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace _00._Work._02._Scripts.UI
{
    public class MenuUIBtn : MonoBehaviour
    {
        [SerializeField] private PlayerInputSo playerInputSo;
        [SerializeField] private GameObject menu;
        [SerializeField] private Button mainButton;
        public Slider bgmSlider;
        public Slider sfxSlider;

        private bool esc;
        private void Awake()
        {
            playerInputSo.ToggleMenu += ToggleMenu;
        }

        private void Start()
        {
            bgmSlider.value = SoundManager.Instance.GetBGMVolume();
            sfxSlider.value = SoundManager.Instance.GetSfxVolume();

            bgmSlider.onValueChanged.AddListener((v) => SoundManager.Instance.SetBgmVolume(v));
            sfxSlider.onValueChanged.AddListener((v) => SoundManager.Instance.SetSfxVolume(v));
            menu.SetActive(false);
        }

        private void OnDisable()
        {
            playerInputSo.ToggleMenu -= ToggleMenu;
        }

        /// <summary>
        /// 메뉴 상태를 토글
        /// </summary>
        private void ToggleMenu()
        {
            esc = !esc; // 상태 반전
            SoundManager.Instance.PlaySfx("buttonPress");
            
            if (esc)
            {
                // 메뉴 켜기
                menu.SetActive(true);
                mainButton.gameObject.SetActive(false);
                Time.timeScale = 0f;
            }
            else
            {
                // 메뉴 끄기
                menu.SetActive(false);
                mainButton.gameObject.SetActive(true);
                Time.timeScale = 1f;
            }
        }

        /// <summary>
        /// 버튼에서 호출 - 메뉴 강제 켜기
        /// </summary>
        public void MainMenu()
        {
            if (!esc) ToggleMenu(); // 꺼져있을 때만 켜기
        }

        /// <summary>
        /// 버튼에서 호출 - 메뉴 강제 끄기
        /// </summary>
        public void ContinueButton()
        {
            if (esc) ToggleMenu(); // 켜져있을 때만 끄기
        }

        public void ExitButton()
        {
            Application.Quit();
        }
    }
}