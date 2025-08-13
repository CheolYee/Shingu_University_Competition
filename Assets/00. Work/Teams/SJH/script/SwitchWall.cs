using System.Collections.Generic;
using UnityEngine;

public class SwitchWall : MonoBehaviour
{
    [SerializeField] private Vector2 boxSize = new Vector2(1f, 1f);
    [SerializeField] private GameObject switchGameObject;
    private Collider2D collider2d;

    void Update()
    {
        if(switchGameObject == null) return;
        collider2d = Physics2D.OverlapBox(switchGameObject.transform.position,
        boxSize, switchGameObject.transform.localRotation.z, 1<<6);
        if(collider2d != null)
        {
            Destroy(gameObject);
        }
    }
}
