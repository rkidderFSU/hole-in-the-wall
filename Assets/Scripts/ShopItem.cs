using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    private SpriteRenderer sr;
    private Color idleColor = new Color(0.5f, 0.5f, 0.5f);
    public GameObject infoPopup;
    public bool inCart;
    
    public CartManager cartManager;


    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = idleColor;
        cartManager = FindObjectOfType<CartManager>();  
    }

    // Update is called once per frame
    void Update()
    {
       /* if (inCart)
        {
            sr.color = Color.white;
        } */
    }

    private void OnMouseEnter()
    {
        // Highlight the item and show info text
        sr.color = Color.white;
        infoPopup.SetActive(true);
    }

    private void OnMouseExit()
    {
        // Hide info text and revert color
        if (inCart == false)
        { 
            sr.color = idleColor; 
        }

        infoPopup.SetActive(false);
    }

    private void OnMouseDown() // Toggles between in and out of cart
    {
        if (inCart == false)
        {
<<<<<<< Updated upstream
            inCart = true;
        }
        else if (inCart == true)
        {
            inCart = false;
            sr.color = idleColor;
        }
    }
=======
            inCart = true; 
            cartManager.AddItemToCart(this);
        }
        else if (inCart == true)
        {
            ResetItem();
        }
        
    }

    public void ResetItem()
    {
        inCart = false;
        sr.color = idleColor; 
    }


>>>>>>> Stashed changes
}
