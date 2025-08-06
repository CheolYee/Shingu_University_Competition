using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Collections;

public class PositionWall : MonoBehaviour
{
    [SerializeField] private List<Transform> location;
    [SerializeField] private float moveTime = 1f;
    private int currentLocation = 0;

    void Start()
    {
        StartCoroutine(MoveWall());
    }

    IEnumerator MoveWall()
    {
        if(gameObject != null)
        {
            transform.DOMove(location[currentLocation].position,moveTime).SetEase(Ease.Linear);
            if(currentLocation == location.Count - 1)
            {
                currentLocation = 0;
            }
            else{
                currentLocation++;
            }
            yield return new WaitForSeconds(moveTime);
            StartCoroutine(MoveWall());
        }
    }


}