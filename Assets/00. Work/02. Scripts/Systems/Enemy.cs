using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public event Action OnDead;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            OnDead?.Invoke();
            Destroy(gameObject);
        }
    }
}
