using _00._Work._02._Scripts.Systems;
using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public event Action OnDead;
    private Bullet bullet;
    private Collider2D collid;
    private Animator ani;

    private void Awake()
    {
        collid = GetComponent<Collider2D>();
        ani = GetComponent<Animator>();
    }

    private void Start()
    {
        bullet = GameObject.FindWithTag("Bullet").GetComponent<Bullet>();
    }

    private void Update()
    {
        if (bullet == null)
        {
            GameObject bullet = GameObject.FindWithTag("Bullet");
            if (bullet != null)
                this.bullet = bullet.GetComponent<Bullet>();
            else
                return;
        }

        collid.isTrigger = bullet.penetration;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            ani.SetBool("Dead", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            ani.SetBool("Dead", true);
        }
    }

    public void DeadAniEvent()
    {
        OnDead?.Invoke();
        Destroy(gameObject);
    }
}
