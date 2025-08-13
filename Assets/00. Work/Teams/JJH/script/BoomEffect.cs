using _00.Work.Scripts.Managers;
using System.Collections;
using UnityEngine;

public class BoomEffect : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(Effect());
    }

    private IEnumerator Effect()
    {
        SoundManager.Instance.PlaySfx("explosion");
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
