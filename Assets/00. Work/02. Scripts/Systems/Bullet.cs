using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rigid;
    public Collider2D collid;

    private Vector3 mouse;
    private Vector3 mousedirection;

    public float speed = 10f;
    private float angle = 0;
    private int currentEffectIndex = 0;

    private bool hasTriggered = false;

    [Header("Card")]
    [SerializeField] private bool bounce = false;
    [SerializeField] public bool penetration = false;
    [SerializeField] private bool speedUp = false;
    [SerializeField] private bool speedDown = false;
    [SerializeField] private bool Explode = false;
    [SerializeField] private GameObject boomEffect;

    [Header("EnumName")]
    [SerializeField] private List<CardEnum> currentSequence;

    private void Awake()
    {
        collid = GetComponent<Collider2D>();
        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0f;
        mousedirection = (mouse - transform.position).normalized;
        angle = Mathf.Atan2(mousedirection.y, mousedirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        collid.isTrigger = false;

        Initialize();
    }

    private void Update()
    {
        SpeedUpDown();
        PenetrationSetting();
        rigid.linearVelocity = mousedirection * speed;
    }


    #region 벽 충돌 판정
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (penetration) return;

        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (Explode) BulletExplode();
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

            if (Explode)
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
            if (Explode) BulletExplode();
            BulletDead();
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            if (penetration)
            {
                return;
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
            collid.isTrigger = false;
            NextBulletPower();
        }
    }
    #endregion


    #region 총알 능력 스왑
    private void NextBulletPower()
    {
        if (currentSequence == null || currentSequence.Count == 0)
        {
            SetBulletPower("All");
            return;
        }

        if (currentEffectIndex >= currentSequence.Count)
        {
            SetBulletPower("All");
            return;
        }

        string effectName = currentSequence[currentEffectIndex].ToString();
        SetBulletPower(effectName);
        currentEffectIndex++;
    }

    private void SetBulletPower(string effect)
    {
        if (!gameObject.activeInHierarchy) return;

        bounce = false;
        penetration = false;
        speedUp = false;
        speedDown = false;
        Explode = false;

        switch (effect)
        {
            case "Bounce":
                bounce = true;
                break;

            case "Penetration":
                penetration = true;
                collid.isTrigger = true;
                break;

            case "SpeedUp":
                StartCoroutine(SpeedEffect(true));
                break;

            case "SpeedDown":
                StartCoroutine(SpeedEffect(false));
                break;

            case "Boom":
                Explode = true;
                break;
        }
    }
    #endregion



    #region 총알 능력
    private void BulletBounce(Collision2D collision)
    {
        Vector2 normal = collision.contacts[0].normal;
        mousedirection = Vector2.Reflect(mousedirection, normal).normalized;
        float angle = Mathf.Atan2(mousedirection.y, mousedirection.x) * Mathf.Rad2Deg;
        rigid.angularVelocity = 0f;
        rigid.rotation = angle;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void PenetrationSetting()
    {
        if (penetration)
            collid.isTrigger = true;
        else
            collid.isTrigger = false;
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
    #endregion




    #region 유지 코루틴
    private IEnumerator DisableCollider()
    {
        collid.enabled = false;
        yield return new WaitForSeconds(0.05f);
        collid.enabled = true;
    }

    private IEnumerator PenetrationTime()
    {
        penetration = true;
        collid.isTrigger = true;

        yield return new WaitForSeconds(0.3f);

        penetration = false;
        collid.isTrigger = false;
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
            SetBulletPower(currentSequence[currentEffectIndex].ToString());
            currentEffectIndex++;
        }
    }
    #endregion
}