using System.Collections;
using UnityEngine;

public class TimeWall : MonoBehaviour
{
    [SerializeField] private float time;
    private SpriteRenderer mySpriteRenderer;
    private Collider2D myCollider;

    void Awake()
    {
        myCollider = GetComponent<Collider2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void DoTime()
    {
        StartCoroutine(Wall());
    }

    public void DoReset()
    {
        myCollider.isTrigger = true;
        mySpriteRenderer.color = new Color32((byte)mySpriteRenderer.color.r,(byte)mySpriteRenderer.color.g,(byte)mySpriteRenderer.color.b,(byte)0.5f);
    }

    IEnumerator Wall()
    {
        yield return new WaitForSeconds(time);
        myCollider.isTrigger = false;
        mySpriteRenderer.color = new Color32((byte)mySpriteRenderer.color.r,(byte)mySpriteRenderer.color.g,(byte)mySpriteRenderer.color.b,1);
        
    }
}
