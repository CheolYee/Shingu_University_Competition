using UnityEngine;

public class SwitchWall : MonoBehaviour
{
    [SerializeField] private Vector2 boxSize = new Vector2(1f, 1f);
    [SerializeField] private GameObject switchGameObject;
    private Collider2D collider2d;
    public static SwitchWall Instance = null;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Update()
    {
        if (switchGameObject == null) return;
        collider2d = Physics2D.OverlapBox(switchGameObject.transform.position,
        boxSize, switchGameObject.transform.localRotation.z, 1 << 6);
        if (collider2d != null)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, boxSize);
    }
}
