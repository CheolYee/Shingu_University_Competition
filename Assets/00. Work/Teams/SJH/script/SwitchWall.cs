using System.Collections.Generic;
using UnityEngine;

public class SwitchWall : MonoBehaviour
{
    [SerializeField] private GameObject switchGameObject;
    private Collider2D myCollider;
    private Collider2D collider2d;

    void Awake()
    {
        myCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if(switchGameObject != null)
        {
            collider2d = Physics2D.OverlapBox(switchGameObject.transform.position, 
            switchGameObject.transform.localScale, switchGameObject.transform.localRotation.z, 1<<6);

        }
        if(collider2d != null)
        {
            Destroy(gameObject);
        }
    }
}
