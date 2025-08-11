using UnityEngine;
using UnityEngine.UI;

namespace _00.Work.Scripts.UI
{
    public class FadeImgFinder : MonoBehaviour
    {
        private void Awake()
        {
            FadeManager.Instance.fadeGroup = gameObject.GetComponent<CanvasGroup>();
            FadeManager.Instance.FadeOut();
        }
    }
}