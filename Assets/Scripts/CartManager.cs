using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartManager : MonoBehaviour
{
    private List<ShopItem> itemsInCart = new List<ShopItem>(); 

    public void AddItemToCart(ShopItem item)
    {
        if (!itemsInCart.Contains(item))
        {
            itemsInCart.Add(item);
        }
    }

    public void ClearCart()
    {
        foreach (ShopItem item in itemsInCart)
        {
            item.ResetItem();
        }
        itemsInCart.Clear(); // Clear the list after resetting items
    }
}

