using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class CustomerScript : MonoBehaviour
{
    public List<GameObject> availableItems; // List of all possible items
    public List<GameObject> currentRequest = new List<GameObject>(); // Items currently requested by the customer
    private UIManager ui;
    private SpriteRenderer sr;
    private CameraController cam;
    public Transform defaultPosition;
    public Transform hidePosition;
    public float speed;

    void Start()
    {
        ui = GameObject.Find("Game Manager").GetComponent<UIManager>();
        sr = GetComponent<SpriteRenderer>();
        cam = GameObject.Find("Main Camera").GetComponent<CameraController>();
        GenerateRequest();
    }

    private void Update()
    {
        
    }

    public void GenerateRequest()
    {
        currentRequest.Clear();

        // Ensure that the items count does not exceed the number of available items
        int maxItemsRequestable = Mathf.Min(3, availableItems.Count); // Cap at 3 or the count of available items, whichever is smaller
        int itemsCount = Random.Range(1, maxItemsRequestable + 1); // Adjust the upper limit to ensure it's within range

        for (int i = 0; i < itemsCount; i++)
        {
            int randomIndex = Random.Range(0, availableItems.Count);
            currentRequest.Add(availableItems[randomIndex]);
            availableItems.RemoveAt(randomIndex); // Remove selected item from the list to prevent duplicates
        }

        LogCurrentRequest();
    }

    public void LogCurrentRequest()
    {
        string requestDescription = "Customer requests: ";

        foreach (GameObject item in currentRequest)
        {
            requestDescription += item.name + ", ";
        }
        requestDescription = requestDescription.TrimEnd(',', ' ');
        StartCoroutine(ui.UpdateRequestText(requestDescription));
    }

    public void LeaveShop()
    {
        sr.flipX = true;
        cam.MoveToFrontOfShop();
        StartCoroutine(MoveToPosition(hidePosition.position));
    }

    public void ReturnToShop()
    {
        sr.flipX = false;
        cam.MoveToFrontOfShop();
        StartCoroutine(MoveToPosition(defaultPosition.position));
    }

    IEnumerator MoveToPosition(Vector2 target)
    {
        while (Vector2.Distance(transform.position, target) > 0.05f)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
            yield return null;
        }
        // Check if the customer is at the default position
        if (Vector3.Distance(transform.position, defaultPosition.position) < 0.05f)
        {
            cam.MoveToPlayer();
            GenerateRequest();
        }
    }
}