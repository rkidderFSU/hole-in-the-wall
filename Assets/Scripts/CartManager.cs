using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartManager : MonoBehaviour
{
    private List<GameObject> itemsInCart = new List<GameObject>();
    public CustomerScript customerRequest;
    public int score = 0;
    private UIManager ui;

    private void Start()
    {
        ui = GameObject.Find("Game Manager").GetComponent<UIManager>();
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
            ui.SetFeedbackText("Item removed: " + item.name);
        }
        else
        {
            itemsInCart.Add(item);
            itemScript.inCart = true;
            sr.color = Color.white;
            ui.SetFeedbackText("Item added: " + item.name);
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
            ui.SetFeedbackText("No items selected");
        }
    }

    public void SellItems()
    {
        if (itemsInCart.Count > 0)
        {
            string correctItems = ""; // String to hold names of correct items
            string incorrectItems = ""; // String to hold names of incorrect items

            foreach (GameObject item in itemsInCart)
            {
                // Check if the item is requested by the customer
                if (customerRequest.currentRequest.Contains(item))
                {
                    score += 10; // Increase score for correct item
                    correctItems += item.name + ", ";
                }
                else
                {
                    score -= 10; // Decrease score for incorrect item
                    incorrectItems += item.name + ", ";
                }
                customerRequest.availableItems.Remove(item); // Removes the item from the list of available items to sell
                item.SetActive(false);
            }
            // Update correct and incorrect item lists as necessary
            if (!string.IsNullOrEmpty(correctItems))
            {
                ui.SetCorrectText("Correct items sold: " + correctItems.TrimEnd(',', ' '));
            }
            if (!string.IsNullOrEmpty(incorrectItems))
            {
                ui.SetIncorrectText("Incorrect items sold: " + incorrectItems.TrimEnd(',', ' '));
            }
            ui.SetFeedbackText("Items sold");

            itemsInCart.Clear(); // Clear the cart after selling items
            customerRequest.GenerateRequest(); // Generate a new request regardless of correct or incorrect items
        }
        else
        {
            ui.SetFeedbackText("No items selected");
        }
    }
}
