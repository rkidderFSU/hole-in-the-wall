using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartManager : MonoBehaviour
{
    private List<GameObject> itemsInCart = new List<GameObject>();

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
            Debug.Log("Item removed: " + item.name);
        }
        else
        {
            itemsInCart.Add(item);
            itemScript.inCart = true;
            sr.color = Color.white;
            Debug.Log("Item added: " + item.name);
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
            Debug.Log("All items removed from cart");
        }
        else
        {
            Debug.Log("No items to remove");
        }
    }

    public void SellItems()
    {
        if (itemsInCart.Count > 0)
        {
            foreach (GameObject item in itemsInCart)
            {
                item.gameObject.SetActive(false);
            }
            itemsInCart.Clear(); // Clear the list after selling items
            Debug.Log("Items sold");
        }
        else
        {
            Debug.Log("No items selected");
        }
    }
}
