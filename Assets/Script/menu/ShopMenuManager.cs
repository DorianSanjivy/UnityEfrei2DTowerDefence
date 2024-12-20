using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMenuManager : MonoBehaviour
{
    public GameObject shopPanel; // Le panel du menu
    public GameObject shopButton;

    private TowerGrid towerGrid;

    void Start() {
        towerGrid = FindObjectOfType<TowerGrid>();
    }

    public void ToggleShop()
    {
        Debug.Log("ToggleShop called");
        shopPanel.SetActive(!shopPanel.activeSelf);
        shopButton.SetActive(!shopButton.activeSelf);
        towerGrid.ToggleActivate();
    }
}

