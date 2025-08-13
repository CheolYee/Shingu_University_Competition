using _00._Work.Teams.PMC._01._Codes;
using UnityEngine;

public class Magnazine : MonoBehaviour
{
    private ClearUIController clearController;
    private Enemy subscribedEnemy;

    private void Awake()
    {
        clearController = GameObject.FindWithTag("clearController").GetComponent<ClearUIController>();
    }

    private void Update()
    {
        TrySubscribe();
    }

    private void OnEnable()
    {
        if (Enemy.Instance != null)
            Enemy.Instance.OnDead += GetMagnazine;
    }

    private void OnDisable()
    {
        if (subscribedEnemy != null)
        {
            subscribedEnemy.OnDead -= GetMagnazine;
            subscribedEnemy = null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            GetMagnazine();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            GetMagnazine();
        }
    }

    private void GetMagnazine()
    {
        if (!isActiveAndEnabled)
            return;
        clearController.AddStarCount();
        gameObject.SetActive(false);
    }

    private void TrySubscribe()
    {
        if (Enemy.Instance == null) return;

        if (subscribedEnemy == Enemy.Instance) return;

        if (subscribedEnemy != null)
            subscribedEnemy.OnDead -= GetMagnazine;

        Enemy.Instance.OnDead += GetMagnazine;
        subscribedEnemy = Enemy.Instance;
    }
}
