using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody2D rigid;
    public Collider2D collidier;
    private Vector3 mouse;
    private Vector3 mousedirection;
    private float angle = 0;

    [SerializeField] private GameObject boomEffect;
    [SerializeField] private bool boom = false;

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
    }

    private void Update()
    {
        rigid.linearVelocity = mousedirection * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (boom) BulletBoom();
            BulletDead();
        }
        else if (collision.gameObject.CompareTag("DeadZone"))
        {
            BulletDead();
        }
    }

    private void BulletBoom()
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
        if (currentSequence == null || currentSequence.Count == 0)
        {
            SetCardSequence(new List<CardEnum>(ReadyButton.lastUsedSequence));
        }
        Debug.Log("[Bullet] 시퀀스 초기화됨: " + string.Join(", ", currentSequence));
    }
    private IEnumerator DeactivateAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}