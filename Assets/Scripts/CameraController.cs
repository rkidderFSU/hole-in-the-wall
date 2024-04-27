using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraController : MonoBehaviour
{
    public Transform playerPosition;
    public Transform shopFrontPosition;
    public Transform playerFollowPosition;

    public float transitionSpeed;
    public float smoothSpeed = 2f;

    public bool transitioning = false;
    public bool followPlayer = true;
    public bool movingToPlayer; // Potential fix for camera movement

    // Start is called before the first frame update
    void Start()
    {
        transform.position = shopFrontPosition.position;
        // StartCoroutine(LockCamera());
    }

    // Update is called once per frame
    void Update()
    {
        if (!transitioning)
        {
            transitionSpeed = smoothSpeed;
        }
        else
        {
            transitionSpeed = 50f;
        }
    }

    void LateUpdate()
    {
        if (followPlayer)
        {
            Vector3 desiredPosition = playerFollowPosition.position;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
    }

    /* private IEnumerator LockCamera()
    {
        while (true)
        {
            if (followPlayer)
            {
                // Move the camera towards the player's follow position
                transform.position = Vector3.Lerp(transform.position, playerFollowPosition.position, 2f * Time.deltaTime);
            }
            yield return null;
        }
    } */


    public void MoveToFrontOfShop()
    {
        followPlayer = false;
        transitioning = true;
        Vector3 targetPosition = shopFrontPosition.position;
        StartCoroutine(TransitionCamera(targetPosition));
    }

    public void MoveToPlayer()
    {
        transitioning = true;
        movingToPlayer = true;
        Vector3 targetPosition = playerFollowPosition.position;
        StartCoroutine(TransitionCamera(targetPosition));
    }

    private IEnumerator TransitionCamera(Vector3 targetPosition)
    {
        Vector3 startPosition = transform.position;
        Vector3 direction = (targetPosition - startPosition).normalized;
        float distance = Vector3.Distance(startPosition, targetPosition);
        float duration = 1f; // Desired duration of transition in seconds
        float t = 0f;

        while (t < 1f)
        {
            float step = distance / duration * Time.deltaTime; // Calculate step size based on distance and desired duration
            transform.Translate(direction * step, Space.World);
            t += step / distance; // Adjust t to maintain progress
            yield return null;
        }

        transform.position = targetPosition; // Ensure we reach the exact target position
        transitioning = false;

        if (movingToPlayer)
        {
            movingToPlayer = false;
            followPlayer = true;
        }
    }
}