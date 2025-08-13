using _00._Work.Teams.PMC._01._Codes.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AbsorptionWall : MonoBehaviour
{
    [SerializeField] private Vector2 boxSize = new Vector2(1f, 1f);
    [SerializeField] private LayerMask bulletLayer;

    private Collider2D hitBullet;

    void Update()
    {
        hitBullet = Physics2D.OverlapBox(transform.position, boxSize, transform.eulerAngles.z, bulletLayer);

        if (hitBullet != null)
        {
            FadeManager.Instance.FadeToScene(SceneManager.GetActiveScene().buildIndex);
            hitBullet.gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, boxSize);
    }
}
