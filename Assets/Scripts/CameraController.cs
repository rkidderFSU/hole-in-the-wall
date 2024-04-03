using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 1f; //how "lazy" the camera is
    public Vector3 offset; //distance between the camera and the player
    
    void Start()
    {
        
    }

    
    void FixedUpdate()
    {
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime); //smoothly move the camera to the desired position

        smoothedPosition.z = transform.position.z;
        transform.position = smoothedPosition;
    }
}
