using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerPosition;
    public Transform shopFrontPosition;
    public Transform playerFollowPosition;

    public float transitionSpeed;

    public bool transitioning = false;
    public bool followPlayer = true;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = shopFrontPosition.position;
        StartCoroutine(LockCamera());
    }

    // Update is called once per frame
    void Update()
    {
        if (!transitioning)
        {
            transitionSpeed = 2f;
        }
        else
        {
            transitionSpeed = 5f;
        }
    }

    private void FixedUpdate()
    {
        
    }

    private IEnumerator LockCamera()
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
    }


    public void MoveToFrontOfShop()
    {
        followPlayer = false;
        transitioning = true;
        Vector3 targetPosition = shopFrontPosition.position;
        StartCoroutine(TransitionCamera(targetPosition));
    }

    public void MoveToPlayer()
    {
        followPlayer = true;
        transitioning = true;
        Vector3 targetPosition = playerFollowPosition.position;
        StartCoroutine(TransitionCamera(targetPosition));
    }

    private IEnumerator TransitionCamera(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.05f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, transitionSpeed * Time.deltaTime);
            yield return null;
        }
        transitioning = false;
    }
}
