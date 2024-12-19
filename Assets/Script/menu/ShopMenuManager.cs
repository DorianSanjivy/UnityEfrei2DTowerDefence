using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMenuManager : MonoBehaviour
{
    public GameObject shopPanel; // Le panel du menu
    public GameObject shopButton;

    public bool activate = false;

    public void ToggleShop()
    {
        shopPanel.SetActive(!shopPanel.activeSelf);
        shopButton.SetActive(!shopButton.activeSelf);
        activate = !activate;
    }
}

