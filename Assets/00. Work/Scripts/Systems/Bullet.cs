using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 10f;
    private Rigidbody2D rigid;
    private Vector3 mouse;
    private Vector3 mousedirection;
    private float angle = 0;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0f;
        mousedirection = (mouse - transform.position).normalized;
        angle = Mathf.Atan2(mousedirection.y, mousedirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    void Update()
    {
        rigid.linearVelocity = mousedirection * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}

