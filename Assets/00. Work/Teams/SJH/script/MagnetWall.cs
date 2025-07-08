using UnityEngine;

public class MagnetWall : MonoBehaviour
{
    [SerializeField] private float maxDistance = 5;
    private Collider2D bullet;

    void Update()
    {
        bullet = Physics2D.OverlapCircle(transform.position, maxDistance, 1<<6);
        
        if(bullet != null && bullet.TryGetComponent(out Bullet b))
        {
            float distance = (transform.position - bullet.transform.position).magnitude;
            float strangth = Mathf.InverseLerp(0, maxDistance, distance)*2;
            Vector3 magnetDir = ((transform.position - bullet.transform.position)/200) * strangth ;
            b.Dir += magnetDir;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, maxDistance);
    }
}
