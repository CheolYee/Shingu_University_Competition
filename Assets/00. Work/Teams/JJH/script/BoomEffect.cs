using _00.Work.Scripts.Managers;
using System.Collections;
using UnityEngine;

public class BoomEffect : MonoBehaviour
{
    private GameObject mag;

    private void Awake()
    {
        mag = GameObject.FindWithTag("Magnazine");
    }

    private void OnEnable()
    {
        if (mag != null)
        {
            mag.SetActive(false);
        }
        StartCoroutine(Effect());
    }

    private IEnumerator Effect()
    {
        SoundManager.Instance.PlaySfx("explosion");
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
