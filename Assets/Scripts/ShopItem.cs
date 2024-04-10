using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    private SpriteRenderer sr;
    private Color idleColor = new Color(0.5f, 0.5f, 0.5f);
    public GameObject infoPopup;
    public bool inCart;
    public List<ShopItem> items;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = idleColor;
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
            inCart = true;
        }
        else if (inCart == true)
        {
            inCart = false;
            sr.color = idleColor;
        }
    }
}
