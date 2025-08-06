using UnityEngine;

public class ExtinctionWall : MonoBehaviour
{

    void Update()
    {
        bool _istriggered = Physics2D.OverlapBox(transform.position,transform.localScale,transform.localEulerAngles.z,1<<6);
        if(_istriggered)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }
}
