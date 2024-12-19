using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacementManager : MonoBehaviour
{
    public static TowerPlacementManager Instance;

    private GameObject selectedTower;

    private void Awake()
    {
        Instance = this;
    }

    public void SelectTower(GameObject tower)
    {
        selectedTower = tower;
    }

    private void Update()
    {
        if (selectedTower != null && Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // Assurez-vous que la tour reste sur le plan
            Instantiate(selectedTower, mousePosition, Quaternion.identity);
            selectedTower = null; // Désélectionner après placement
        }
    }
}
