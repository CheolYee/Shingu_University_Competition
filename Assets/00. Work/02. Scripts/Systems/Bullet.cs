using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rigid;
    public Collider2D collidier;

    private Vector3 mouse;
    private Vector3 mousedirection;

    public float speed = 10f;
    private float angle = 0;
    private int currentEffectIndex = 0;

    [Header("Card")]
    [SerializeField] private bool bounce = false;
    [SerializeField] private bool penetration = false;
    [SerializeField] private bool speedUp = false;
    [SerializeField] private bool speedDown = false;
    [SerializeField] private bool Explode = false;
    [SerializeField] private GameObject boomEffect;

    [Header("EnumName")]
    [SerializeField] private List<CardEnum> currentSequence;

    private void Awake()
    {
        collidier = GetComponent<Collider2D>();
        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0f;
        mousedirection = (mouse - transform.position).normalized;
        angle = Mathf.Atan2(mousedirection.y, mousedirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        collidier.isTrigger = false;

        Initialize(); //항상 활성화될 때 시퀀스 반영
    }

    private void Update()
    {
        rigid.linearVelocity = mousedirection * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (Explode)
            {
                BulletExplode();
            }
            BulletDead();
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            NextBulletPower();
            if (bounce)
            {
                BulletBounce(collision);
            }
            else if (Explode)
            {
                BulletExplode();
            }
            else
            {
                BulletDead();
            }

        }
        else if (collision.gameObject.CompareTag("DeadZone"))
        {
            BulletDead();
        }
    }


    private void NextBulletPower()
    {
        if (currentSequence == null || currentSequence.Count == 0 || currentEffectIndex >= currentSequence.Count)
            return;

        string effectName = currentSequence[currentEffectIndex].ToString();
        SetBulletPower(effectName);

        currentEffectIndex++;
    }

    private void SetBulletPower(string effect)
    {
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
                break;
            case "SpeedUp":
                speedUp = true;
                break;
            case "SpeedDown":
                speedDown = true;
                break;
            case "Boom":
                Explode = true;
                break;
        }
    }

    private void BulletBounce(Collision2D collision)
    {
        collision.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        Vector2 normal = collision.contacts[0].normal;
        mousedirection = Vector2.Reflect(mousedirection, normal).normalized;
        float angle = Mathf.Atan2(mousedirection.y, mousedirection.x) * Mathf.Rad2Deg;
        rigid.angularVelocity = 0f;
        rigid.rotation = angle;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void BulletExplode()
    {
        Instantiate(boomEffect, transform.position, Quaternion.identity);
    }

    private void BulletDead()
    {
        gameObject.SetActive(false);
    }

    public void SetCardSequence(List<CardEnum> sequence)
    {
        currentSequence = new List<CardEnum>(sequence);
        Debug.Log("=== 불렛 시퀀스 저장됨 === " + string.Join(", ", currentSequence));
    }

    public void Initialize()
    {
        // 🔹 매번 최신 시퀀스로 덮어쓰기
        currentSequence = new List<CardEnum>(ReadyButton.lastUsedSequence);
        Debug.Log("[Bullet] 시퀀스 초기화됨: " + string.Join(", ", currentSequence));
    }

    private IEnumerator DeactivateAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}