using _00._Work._02._Scripts.Systems;
using UnityEngine;

public class MagnetWall : MonoBehaviour
{
    [SerializeField] private float maxDistance = 5;
    private Collider2D bulletCollid;

    void Update()
    {
        bulletCollid = Physics2D.OverlapCircle(transform.position, maxDistance, 1 << 6);

        if (bulletCollid != null && bulletCollid.TryGetComponent(out Bullet b))
        {
            float distance = (transform.position - bulletCollid.transform.position).magnitude;
            float strangth = Mathf.InverseLerp(0, maxDistance, distance) * 2;
            Vector3 magnetDir = ((transform.position - bulletCollid.transform.position) / 200) * strangth;
            //b.Dir += magnetDir;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, maxDistance);
    }
}
