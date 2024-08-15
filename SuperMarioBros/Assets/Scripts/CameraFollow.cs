using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float minX, maxX, minY, maxY, timeLerp;
    private void FixedUpdate()
    {
        Vector3 newPosition = player.position + new Vector3(0 ,0 , -10);
        newPosition.y = Mathf.Clamp(player.position.y,minY,maxY);
        newPosition = Vector3.Lerp(transform.position, newPosition, timeLerp);
        transform.position = newPosition;

        float eixoX = Mathf.Clamp(transform.position.x,minX,maxX);
        transform.position = new Vector3(eixoX, transform.position.y, transform.position.z);
    }
}
