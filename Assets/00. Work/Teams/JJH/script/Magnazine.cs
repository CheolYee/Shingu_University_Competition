using _00._Work.Teams.PMC._01._Codes;
using UnityEngine;

public class Magnazine : MonoBehaviour
{
    private ClearUIController clearController;

    private void Awake()
    {
        clearController = GameObject.FindWithTag("clearController").GetComponent<ClearUIController>();
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
        clearController.AddStarCount();
        gameObject.SetActive(false);
    }
}
