using System.Collections;
using UnityEngine;

public class TimeWall : MonoBehaviour
{
    public static TimeWall Instance = null;
    [SerializeField] private float time;
    private SpriteRenderer mySpriteRenderer;
    private Collider2D myCollider;

    void Awake()
    {
        myCollider = GetComponent<Collider2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        if (Instance == null)
            Instance = this;
        DoReset();
    }

    public void DoTime()
    {
        StartCoroutine(Wall());
    }

    public void DoReset()
    {
        myCollider.isTrigger = true;
        Physics2D.IgnoreLayerCollision(6, 8, true);
        Color c = mySpriteRenderer.color;
        c.a = 0.5f;
        mySpriteRenderer.color = c;
    }

    IEnumerator Wall()
    {
        yield return new WaitForSeconds(time);
        myCollider.isTrigger = false;
        Physics2D.IgnoreLayerCollision(6, 8, false);
        Color c = mySpriteRenderer.color;
        c.a = 1f;
        mySpriteRenderer.color = c;
    }
}
