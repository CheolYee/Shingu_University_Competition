using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _00._Work._02._Scripts.Systems
{
    public class Bullet : MonoBehaviour
    {
        private Rigidbody2D rigid;
        public Collider2D collide;
        private TrailRenderer trail;

        private Vector3 mouse;
        private Vector3 mouseDir;

        public float speed = 10f;
        private float angle;
        private int currentEffectIndex;

        [Header("Card")]
        [SerializeField] private bool bounce;
        [SerializeField] public bool penetration;
        [SerializeField] private bool speedUp;
        [SerializeField] private bool speedDown;
        [SerializeField] private bool explode;
        [SerializeField] public bool magnet;
        [SerializeField] private GameObject boomEffect;

        [Header("EnumName")]
        [SerializeField] private List<CardEnum> currentSequence;

        private void Awake()
        {
            collide = GetComponent<Collider2D>();
            rigid = GetComponent<Rigidbody2D>();
            trail = GetComponent<TrailRenderer>();
        }

        private void OnEnable()
        {
            if (Camera.main != null) mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouse.z = 0f;
            mouseDir = (mouse - transform.position).normalized;
            angle = Mathf.Atan2(mouseDir.y, mouseDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
            collide.isTrigger = false;

            Initialize();
        }

        private void Update()
        {
            SpeedUpDown();
            PenetrationSetting();
            rigid.linearVelocity = mouseDir * speed;
        }


        #region 벽 충돌 판정
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (penetration) return;

            if (collision.gameObject.CompareTag("Enemy"))
            {
                if (explode) BulletExplode();
                BulletDead();
                return;
            }

            if (collision.gameObject.CompareTag("Wall"))
            {
                if (bounce)
                {
                    BulletBounce(collision);
                    StartCoroutine(DisableCollider());
                    NextBulletPower();
                    return;
                }

                if (explode)
                {
                    BulletExplode();
                    BulletDead();
                    NextBulletPower();
                    return;
                }

                BulletDead();
                NextBulletPower();
                return;
            }

            if (collision.gameObject.CompareTag("DeadZone"))
            {
                BulletDead();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                if (explode) BulletExplode();
                BulletDead();
            }
            else if (collision.gameObject.CompareTag("Wall"))
            {
                if (penetration)
                {
                }
                else
                {
                    BulletDead();
                    NextBulletPower();
                }
            }
            else if (collision.gameObject.CompareTag("DeadZone"))
            {
                BulletDead();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (penetration && collision.CompareTag("Wall"))
            {
                penetration = false;
                collide.isTrigger = false;
                NextBulletPower();
            }
        }
        #endregion

        #region 총알 능력 스왑
        private void NextBulletPower()
        {
            if (currentSequence == null || currentSequence.Count == 0)
            {
                SetBulletPower();
                return;
            }

            if (currentEffectIndex >= currentSequence.Count)
            {
                SetBulletPower();
                return;
            }

            CardEnum effect = currentSequence[currentEffectIndex];
            SetBulletPower(effect);
            currentEffectIndex++;
        }

        private void SetBulletPower(CardEnum effect = CardEnum.Default)
        {
            if (!gameObject.activeInHierarchy) return;

            bounce = false;
            penetration = false;
            speedUp = false;
            speedDown = false;
            explode = false;
            magnet = false;

            switch (effect)
            {
                case CardEnum.Bounce:
                    bounce = true;
                    ChangeTrailColorSimple("#00FFFF");
                    break;

                case CardEnum.Penetration:
                    penetration = true;
                    collide.isTrigger = true;
                    ChangeTrailColorSimple("#FF00FF");
                    break;

                case CardEnum.SpeedUp:
                    StartCoroutine(SpeedEffect(true));
                    break;

                case CardEnum.SpeedDown:
                    StartCoroutine(SpeedEffect(false));
                    break;

                case CardEnum.Explode:
                    explode = true;
                    ChangeTrailColorSimple("#8000FF");
                    break;

                case CardEnum.Magnet:
                    MagnetTrail();
                    magnet = true;
                    break;

                case CardEnum.Default:
                    ChangeTrailColorSimple("#FFFFFF");
                    break;
            }
        }
        #endregion

        #region 총알 능력
        private void BulletBounce(Collision2D collision)
        {
            Vector2 normal = collision.contacts[0].normal;
            mouseDir = Vector2.Reflect(mouseDir, normal).normalized;
            float rotation = Mathf.Atan2(mouseDir.y, mouseDir.x) * Mathf.Rad2Deg;
            rigid.angularVelocity = 0f;
            rigid.rotation = rotation;
            transform.rotation = Quaternion.Euler(0, 0, rotation);
        }

        private void PenetrationSetting()
        {
            collide.isTrigger = penetration;
        }

        private void SpeedUpDown()
        {
            if (speedUp)
                speed = 13f;
            else if (speedDown)
                speed = 7f;
            else if (!speedUp && !speedDown)
                speed = 10f;
        }

        private void BulletExplode()
        {
            Instantiate(boomEffect, transform.position, Quaternion.identity);
        }

        private void BulletDead()
        {
            gameObject.SetActive(false);
        }

        private void ChangeTrailColorSimple(string stringColor)
        {
            if (UnityEngine.ColorUtility.TryParseHtmlString(stringColor, out Color color))
            {
                var trail = GetComponent<TrailRenderer>();
                trail.startColor = color;
                trail.endColor = color;
            }
        }

        private void MagnetTrail()
        {
            var trail = GetComponent<TrailRenderer>();
            Color MagnetColor = new Color(1f - trail.startColor.r, 1f - trail.startColor.g, 1f - trail.startColor.b, trail.startColor.a);
            trail.startColor = MagnetColor;
            trail.endColor = MagnetColor;
        }
        #endregion

        #region 유지 코루틴
        private IEnumerator DisableCollider()
        {
            collide.enabled = false;
            yield return new WaitForSeconds(0.05f);
            collide.enabled = true;
        }

        private IEnumerator SpeedEffect(bool isSpeedUp)
        {
            if (isSpeedUp)
                speedUp = true;
            else
                speedDown = true;

            yield return new WaitForSeconds(1f);

            if (isSpeedUp) speedUp = false;
            else speedDown = false;
        }
        #endregion

        #region 카드 받아오기
        public void SetCardSequence(List<CardEnum> sequence)
        {
            currentSequence = new List<CardEnum>(sequence);
            Debug.Log("=== 불렛 시퀀스 저장됨 === " + string.Join(", ", currentSequence));
        }

        public void Initialize()
        {
            currentEffectIndex = 0;
            currentSequence = new List<CardEnum>(ReadyButton.lastUsedSequence);
            Debug.Log("[Bullet] 시퀀스 초기화됨: " + string.Join(", ", currentSequence));

            if (currentSequence.Count > 0)
            {
                SetBulletPower(currentSequence[currentEffectIndex]);
                currentEffectIndex++;
            }
        }
        #endregion

        #region 자석 벽 
        public Vector3 Dir
        {
            get
            {
                return mouseDir;
            }

            set
            {
                mouseDir = value;
            }
        }
        #endregion
    }
}