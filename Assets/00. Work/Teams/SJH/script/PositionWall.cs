using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionWall : MonoBehaviour
{
    [SerializeField] private List<Transform> location;
    [SerializeField] private float moveTime = 1f;
    [SerializeField] private bool isRotation = false;
    [SerializeField] private bool isRotationSelect = false;
    [SerializeField] private float rotationSeclectValue = 0f;
    [SerializeField] private float rotationSeclectSpeed = 0f;

    private int currentLocation = 0;

    void Start()
    {
        if (!isRotation && !isRotationSelect)
            StartCoroutine(MoveWall());
    }

    private void Update()
    {
        if (isRotation && !isRotationSelect)
            transform.Rotate(0f, 0f, 90f * Time.deltaTime);
        else if (!isRotation && isRotationSelect)
        {
            float angle = Mathf.Sin(Time.time * rotationSeclectSpeed) * rotationSeclectValue;
            transform.localRotation = Quaternion.Euler(0f, 0f, angle);
        }
    }

    IEnumerator MoveWall()
    {
        if (gameObject != null)
        {
            transform.DOMove(location[currentLocation].position, moveTime).SetEase(Ease.Linear);
            if (currentLocation == location.Count - 1)
            {
                currentLocation = 0;
            }
            else
            {
                currentLocation++;
            }
            yield return new WaitForSeconds(moveTime);
            StartCoroutine(MoveWall());
        }
    }
}