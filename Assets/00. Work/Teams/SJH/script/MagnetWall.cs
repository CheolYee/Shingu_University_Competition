using _00._Work._02._Scripts.Systems;
using UnityEngine;

public class MagnetWall : MonoBehaviour
{
    [SerializeField] private float maxDistance = 5;
    private Collider2D bulletCollid;
    private Bullet bullet;
    private float magnetfloat = 1f;

    void Update()
    {
        if (bullet == null)
        {
            GameObject bulletObj = GameObject.FindWithTag("Bullet");
            if (bulletObj != null && bulletObj.activeInHierarchy)
                bullet = bulletObj.GetComponent<Bullet>();
            else
                return;
        }
        magnetfloat = bullet.magnet ? -1f : 1f;

        bulletCollid = Physics2D.OverlapCircle(transform.position, maxDistance, 1 << 6);
        if (bulletCollid != null && bulletCollid.TryGetComponent(out Bullet b))
        {
            float distance = (transform.position - bulletCollid.transform.position).magnitude;
            float strangth = Mathf.InverseLerp(0, maxDistance, distance) * 0.5f;
            Vector3 magnetDir = magnetfloat * ((transform.position - bulletCollid.transform.position) / 200) * strangth;
            b.Dir += magnetDir;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, maxDistance);
    }
}
