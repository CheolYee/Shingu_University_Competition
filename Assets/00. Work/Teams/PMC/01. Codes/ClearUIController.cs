using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _00._Work.Teams.PMC._01._Codes
{
    public class ClearUIController : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private GameObject starPanel;
        [SerializeField] private Image[] starSlots;
        [SerializeField] private GameObject starPrefab;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private GameObject shatterEffectPrefab;
        [SerializeField] private Button clearButton;
        
        [SerializeField] private Transform[] bulletStartPoints;
        [SerializeField] private float bulletArrivalTime;
        [SerializeField] private Vector2 gizmoRadius;
        
        [Header("StarCounts")]
        [SerializeField] private int starCount = 3;

        public void SubscribeToEnemy(Enemy enemy)
        {
            enemy.OnDead += () => ShowStars(starCount);
        }
        
        public void ShowStars(int count)
        {
            starPanel.SetActive(true);
            clearButton.gameObject.SetActive(false);

            StartCoroutine(StartEffectFlow(count));
        }

        private IEnumerator StartEffectFlow(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Vector3 targetPos = starSlots[i].transform.position; //별 생성

                // 총알 RectTransform 이동 설정
                RectTransform startRect = bulletStartPoints[i].GetComponent<RectTransform>();
                RectTransform slotRect = starSlots[i].GetComponent<RectTransform>();

                // 정확한 위치를 위해 WorldPosition으로 총알 생성
                Vector3 startWorldPos = startRect.position;
                Vector3 targetWorldPos = slotRect.position;

                GameObject bullet = Instantiate(bulletPrefab, startWorldPos, Quaternion.identity, starPanel.transform);
                bullet.transform.DOMove(targetWorldPos, bulletArrivalTime).SetEase(Ease.OutQuad);

                yield return new WaitForSeconds(bulletArrivalTime);
                
                Destroy(bullet);
                
                // 랜덤 오프셋 계산 (예: -10 ~ +10 px 범위)
                Vector2 randomOffset = new Vector2(
                    Random.Range(-10f, 10f),
                    Random.Range(-10f, 10f)
                );

                Vector3 shatterPos = targetPos + (Vector3)randomOffset;

                // 파편 이펙트 생성
                GameObject shatter = Instantiate(shatterEffectPrefab, shatterPos, Quaternion.identity, starPanel.transform);
                RectTransform shatterRect = shatter.GetComponent<RectTransform>();
                CanvasGroup shatterCanvas = shatter.GetComponent<CanvasGroup>();

                shatterRect.localScale = Vector3.zero;
                shatterCanvas.alpha = 1f;

                shatterRect.DOScale(1.5f, 0.2f).SetEase(Ease.OutBack);
                shatterCanvas.DOFade(0f, 0.5f).SetEase(Ease.OutQuad).SetDelay(0.1f).OnComplete(() =>
                {
                    Destroy(shatter);
                });

                GameObject star = Instantiate(starPrefab, targetPos,
                    Quaternion.identity, starSlots[i].transform);
                //별 등장 애니메이션
                star.transform.DOScale(1.2f, 0.3f).SetEase(Ease.OutBack).OnComplete(() =>
                {
                    star.transform.DOScale(1f, 0.1f);
                });
                
                yield return new WaitForSeconds(0.4f);
            }
            
            clearButton.gameObject.SetActive(true);
        }
#if  UNITY_EDITOR
        
        private void OnDrawGizmos()
        {
            if (bulletStartPoints == null) return;
            
            foreach (var trm in starSlots)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawCube(trm.transform.position, gizmoRadius);
            }

            foreach (var trm in bulletStartPoints)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawCube(trm.position, gizmoRadius);
            }
        }
#endif
    }
}
