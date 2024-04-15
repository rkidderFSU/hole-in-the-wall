using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class CustomerScript : MonoBehaviour
{
    public List<GameObject> availableItems; // List of all possible items
    public List<GameObject> currentRequest = new List<GameObject>(); // Items currently requested by the customer
    private UIManager ui;

    void Start()
    {
        ui = GameObject.Find("Game Manager").GetComponent<UIManager>();
        GenerateRequest();
    }

    public void GenerateRequest()
    {
        currentRequest.Clear();
        int itemsCount = Random.Range(1, 4); // Gets a random number between 1 and 3

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
        ui.UpdateRequestText(requestDescription);
        
    }
}