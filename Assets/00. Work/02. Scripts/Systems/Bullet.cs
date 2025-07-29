using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Accessibility;

public class Bullet : MonoBehaviour
{
    private float speed = 10f;
    private Rigidbody2D rigid;
    private Collider2D collidier;
    private Vector3 mouse;
    private Vector3 mousedirection;
    private float angle = 0;
    [SerializeField] private GameObject boomEffect;

    [Header("Card")]
    [SerializeField] private bool bounce = false;
    [SerializeField] private bool penetration = false;
    [SerializeField] private bool speedUp = false;
    [SerializeField] private bool speedDown = false;
    [SerializeField] private bool boom = false;

    [Header("Slots")]
    [SerializeField] private List<CardEnum> currentSequence = new List<CardEnum>();

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

        if (bounce)
            collidier.isTrigger = false;
        else if (penetration)
            collidier.isTrigger = true;
        else if (speedUp)
            speed = 13f;
        else if (speedDown)
            speed = 7f;
    }

    void Update()
    {
        rigid.linearVelocity = mousedirection * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (boom)
            {
                BulletBoom();
            }
            BulletDead();
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            if (bounce)
            {
                BulletBounce(collision);
            }
            else if (boom)
            {
                BulletBoom();
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

    private void BulletBoom()
    {
        GameObject boomeffect = Instantiate(boomEffect, transform.position, Quaternion.identity);
    }

    private void BulletDead()
    {
        gameObject.SetActive(false);
    }

    public Vector3 Dir
    {
        get
        {
            return mousedirection;
        }

        set
        {
            mousedirection = value;
        }
    }


    // 홍지후 스크립트
    public void SetCardSequence(List<CardEnum> sequence)
    {
        currentSequence = new List<CardEnum>(sequence);
        Debug.Log("받은 카드 시퀀스: " + string.Join(", ", currentSequence));
    }

    private bool HasCard(CardEnum type)
    {
        return currentSequence.Contains(type);
    }
    // 홍지후 스크립트
}




