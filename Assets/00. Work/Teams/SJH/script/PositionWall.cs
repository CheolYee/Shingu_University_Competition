using System.Collections.Generic;
using UnityEngine;

public class PositionWall : MonoBehaviour
{
    [SerializeField] private List<Transform> location;
    [SerializeField] private float changeTime = 1f;
    private int currentLocation = 0;
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position,location[currentLocation].position, 0);
        if(currentLocation == location.Count)
        {
            currentLocation = 0;
        }
        else
        {
            currentLocation++;
        }
    }
}