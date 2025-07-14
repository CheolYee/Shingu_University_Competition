using UnityEngine;

public class AbsorptionWall : MonoBehaviour
{
    private Collider2D hitBullet;
    void Update()
    {
        hitBullet = Physics2D.OverlapBox(transform.position, transform.localScale,transform.localRotation.z);
        Destroy(hitBullet.gameObject);
    }
}
