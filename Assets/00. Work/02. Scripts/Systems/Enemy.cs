using System;
using _00._Work._02._Scripts.Systems;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public event Action OnDead;
    private Bullet bullet;
    private Collider2D collid;

    private void Awake()
    {
        collid = GetComponent<Collider2D>();
        bullet = GameObject.FindWithTag("Bullet").GetComponent<Bullet>();
    }

    private void Update()
    {
        if (bullet.penetration)
            collid.isTrigger = true;
        else
            collid.isTrigger = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            OnDead?.Invoke();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            OnDead?.Invoke();
            Destroy(gameObject);
        }
    }
}
