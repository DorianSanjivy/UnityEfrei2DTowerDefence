using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMenuManager : MonoBehaviour
{
    public GameObject shopPanel; // Le panel du menu
    public GameObject shopButton;

    public void ToggleShop()
    {
        shopPanel.SetActive(!shopPanel.activeSelf);
        shopButton.SetActive(!shopButton.activeSelf);
    }
}

