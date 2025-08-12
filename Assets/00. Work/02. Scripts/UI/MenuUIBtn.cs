using _00.Work.Scripts.Managers;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace _00.Work.Scripts.UI
{
    public class MenuUIBtn : MonoBehaviour
    {
        [SerializeField] private GameObject menu;
        [SerializeField] private Button mainButton;
        public Slider bgmSlider;
        public Slider sfxSlider;

        private bool _isPressedEsc;
        private bool esc = false;

        private void Start()
        {
            bgmSlider.value = SoundManager.Instance.GetBGMVolume();
            sfxSlider.value = SoundManager.Instance.GetSfxVolume();

            bgmSlider.onValueChanged.AddListener((v) => SoundManager.Instance.SetBgmVolume(v));
            sfxSlider.onValueChanged.AddListener((v) => SoundManager.Instance.SetSfxVolume(v));
            menu.SetActive(false);
        }

        private void Update()
        {
            if (Keyboard.current.escapeKey.isPressed)
                esc = true;

            if (esc)
            {
                menu.SetActive(true);
                mainButton.gameObject.SetActive(false);
                Time.timeScale = 0;
            }
        }

        public void MainMenu()
        {
            esc = true;
        }

        public void ContinueButton()
        {
            _isPressedEsc = !_isPressedEsc;
            esc = false;
            Time.timeScale = 1;
            mainButton.gameObject.SetActive(true);
            menu.SetActive(false);
        }

        public void ExitButton()
        {
            Application.Quit();
        }
    }
}