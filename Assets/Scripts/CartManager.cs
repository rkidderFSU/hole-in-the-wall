using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartManager : MonoBehaviour
{
    private List<GameObject> itemsInCart = new List<GameObject>();
    public CustomerScript customer;
    public int score = 0;
    private UIManager ui;
    private CameraController cam;
    private GameManager m;

    private void Start()
    {
        ui = GameObject.Find("Game Manager").GetComponent<UIManager>();
        cam = GameObject.Find("Main Camera").GetComponent<CameraController>();
        m = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    public void ToggleItem(GameObject item)
    {
        // Grabs each item's Sprite Renderer and ShopItem script
        SpriteRenderer sr = item.GetComponent<SpriteRenderer>();
        ShopItem itemScript = item.GetComponent<ShopItem>();

        if (itemsInCart.Contains(item))
        {
            itemsInCart.Remove(item);
            itemScript.inCart = false;
            sr.color = itemScript.idleColor;
            ui.SetFeedbackText("Item Removed: " + item.name);
        }
        else
        {
            itemsInCart.Add(item);
            itemScript.inCart = true;
            sr.color = Color.white;
            ui.SetFeedbackText("Item Added: " + item.name);
        }
    }

    public void ClearCart()
    {
        if (itemsInCart.Count > 0)
        {
            foreach (GameObject item in itemsInCart)
            {
                SpriteRenderer sr = item.GetComponent<SpriteRenderer>();
                ShopItem itemScript = item.GetComponent<ShopItem>();

                sr.color = itemScript.idleColor;
                itemScript.inCart = false;
            }
            itemsInCart.Clear(); // Clear the list after resetting items
            ui.SetFeedbackText("All items removed from cart");
        }
        else
        {
            ui.SetFeedbackText("No Items Selected");
        }
    }

    public void SellItems()
    {
        if (itemsInCart.Count > 0)
        {
            ProcessSale();
            customer.LeaveShop();
            StartCoroutine(WaitAndReturnCustomer(5.0f));
        }
        else
        {
            ui.SetFeedbackText("No Items Selected");
        }
    }

    public void ProcessSale()
    {
        List<GameObject> soldItems = new List<GameObject>();
        string correctItems = ""; // String to hold names of correct items
        string incorrectItems = ""; // String to hold names of incorrect items
        string unsoldItems = ""; //String to hold names of unsold items

        foreach (GameObject item in itemsInCart)
        {
            // Check if the item is requested by the customer
            if (customer.currentRequest.Contains(item))
            {
                score += 20; // Increase score for correct item
                correctItems += item.name + ", ";
            }
            else
            {
                score -= 10; // Decrease score for incorrect item
                incorrectItems += item.name + ", ";
            }
            soldItems.Add(item);
            item.SetActive(false);
        }

        // Add unsold requested items back to the available list
        foreach (GameObject item in customer.currentRequest)
        {
            if (!itemsInCart.Contains(item)) // Check if the item was not sold
            {
                unsoldItems += item.name + ", ";
                score -= 5; // Decrease score for each requested item not sold
            }
            else
            {
                customer.availableItems.Add(item); // Add sold items back to available items
            }
        }

        // Update UI as necessary
        if (!string.IsNullOrEmpty(correctItems))
        {
            ui.SetCorrectText("Correct Items Sold: " + correctItems.TrimEnd(',', ' '));
        }
        if (!string.IsNullOrEmpty(incorrectItems))
        {
            ui.SetIncorrectText("Incorrect Items Sold: " + incorrectItems.TrimEnd(',', ' '));
        }
        if (!string.IsNullOrEmpty(unsoldItems))
        {
            ui.SetUnsoldText("Items Not Sold: " + unsoldItems.TrimEnd(',', ' '));
        }
        ui.SetFeedbackText("Items Sold");

        // Remove sold items from available items and current request
        foreach (GameObject soldItem in soldItems)
        {
            customer.currentRequest.Remove(soldItem);
            customer.availableItems.Remove(soldItem);
        }

        itemsInCart.Clear(); // Clear the cart after selling items
    }

    IEnumerator WaitAndReturnCustomer(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (customer.availableItems.Count > 0)
        {
            customer.ReturnToShop();
        }
        else
        {
            cam.MoveToPlayer();
            m.gameEnded = true;
            ui.DisplayEndScreen();
        }
    }
}
