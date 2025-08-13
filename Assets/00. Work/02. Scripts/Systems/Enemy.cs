using _00._Work._02._Scripts.Systems;
using _00.Work.Scripts.Managers;
using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Enemy Instance = null;
    public event Action OnDead;
    private Magnazine magnazine;
    private Bullet bullet;
    private Collider2D collid;
    private Animator ani;

    private void Awake()
    {
        magnazine = GameObject.FindWithTag("Magnazine").GetComponent<Magnazine>();
        collid = GetComponent<Collider2D>();
        ani = GetComponent<Animator>();
        if (Instance == null)
            Instance = this;
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
            Dead();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Dead();
        }
    }

    private void Dead()
    {
        ani.SetBool("Dead", true);
        Gun.Instance.magnazineget = true;
        Gun.Instance.BulletDead?.Invoke();
        SoundManager.Instance.PlaySfx("EnemyDead");
    }

    public void DeadAniEvent()
    {
        OnDead?.Invoke();
        Destroy(gameObject);
    }
}
