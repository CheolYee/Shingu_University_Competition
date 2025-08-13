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
        
        [Header("OnDrowGizmo")]
        [SerializeField] private bool onDrowGizmo;
        
        [SerializeField] private Transform[] bulletStartPoints;
        [SerializeField] private float bulletArrivalTime;
        [SerializeField] private Vector2 gizmoRadius;
        
        [Header("StarCounts")]
        private int starCount = 1;

        public void SubscribeToEnemy(Enemy enemy)
        {
            enemy.OnDead += () => ShowStars(Mathf.Clamp(starCount, 1, 3));
        }

        public void AddStarCount()
        {
            starCount++;
        }

        public void ResetStarCount()
        {
            starCount = 0;
        }
        
        public void ShowStars(int count)
        {
            starPanel.SetActive(true);
            clearButton.gameObject.SetActive(false);

            StartCoroutine(StartEffectFlow(count));
        }

        private IEnumerator StartEffectFlow(int count)
        {
            yield return null;
            for (int i = 0; i < count; i++)
            {
                Vector3 targetPos = starSlots[i].transform.position; //별 생성 위치

                // 총알 RectTransform 이동 설정
                RectTransform startRect = bulletStartPoints[i].GetComponent<RectTransform>();
                RectTransform slotRect = starSlots[i].GetComponent<RectTransform>();

                // 정확한 위치를 위해 WorldPosition으로 총알 생성
                Vector3 worldStartPos = startRect.position;
                Vector3 worldTargetPos = slotRect.position;

                GameObject bullet = Instantiate(bulletPrefab, worldStartPos, Quaternion.identity, starPanel.transform);
                bullet.transform.position = worldStartPos;

                bullet.transform.DOMove(worldTargetPos, bulletArrivalTime).SetEase(Ease.OutQuad);

                yield return new WaitForSeconds(bulletArrivalTime);
                
                Destroy(bullet);
                
                // 랜덤 오프셋을 UI 로컬 좌표가 아니라 월드 좌표 기준으로 변환
                Vector3 targetWorldPos = starSlots[i].GetComponent<RectTransform>().position;
                Vector3 randomWorldOffset = new Vector3(
                    Random.Range(-10f, 10f) * starPanel.transform.lossyScale.x,
                    Random.Range(-10f, 10f) * starPanel.transform.lossyScale.y,
                    0f
                );

                // 월드 좌표로 파편 생성
                Vector3 shatterWorldPos = targetWorldPos + randomWorldOffset;

                GameObject shatter = Instantiate(shatterEffectPrefab, shatterWorldPos, Quaternion.identity, starPanel.transform);
                RectTransform shatterRect = shatter.GetComponent<RectTransform>();
                CanvasGroup shatterCanvas = shatter.GetComponent<CanvasGroup>();

                shatterRect.localScale = Vector3.zero;
                shatterCanvas.alpha = 1f;

                shatterRect.DOScale(1.5f, 0.2f).SetEase(Ease.OutBack);
                shatterCanvas.DOFade(0f, 0.5f)
                    .SetEase(Ease.OutQuad)
                    .SetDelay(0.1f)
                    .OnComplete(() => Destroy(shatter));

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
            if (bulletStartPoints == null || onDrowGizmo == false) return;
            
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
