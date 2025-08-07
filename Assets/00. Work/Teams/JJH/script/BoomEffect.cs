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
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
