using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 10f;
    private Rigidbody2D rigid;
    private Vector3 mouse;
    private Vector3 mousedirection;
    private float angle = 0;

    [SerializeField] private bool bounce = false;
    [SerializeField] private bool penetration = false;
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

        if (bounce)
            Physics2D.IgnoreLayerCollision(6, 7, false);
        else if (penetration)
            Physics2D.IgnoreLayerCollision(6, 7, true);
    }

    void Update()
    {
        rigid.linearVelocity = mousedirection * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            if (bounce)
            {
                collision.gameObject.GetComponent<BoxCollider2D>().enabled = true;
                Vector2 normal = collision.contacts[0].normal;
                mousedirection = Vector2.Reflect(mousedirection, normal).normalized;
                float angle = Mathf.Atan2(mousedirection.y, mousedirection.x) * Mathf.Rad2Deg;
                rigid.angularVelocity = 0f;
                rigid.rotation = angle;
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
        else if (collision.gameObject.CompareTag("DeadZone"))
        {
            gameObject.SetActive(false);
        }
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
}

