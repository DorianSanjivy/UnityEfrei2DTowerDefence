using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerButton : MonoBehaviour
{
    public string towerName;
    public int price;
    public int damage;
    public Text infoText; // Texte pour afficher les informations

    public GameObject towerPrefab; // Tour � placer

    private void Start()
    {
        // Configurez le texte du bouton avec le nom de la tour
        GetComponentInChildren<Text>().text = towerName;
    }

    public void ShowInfo()
    {
        infoText.text = $"Nom : {towerName}\nPrix : {price}\nD�g�ts : {damage}";
    }

    public void OnClick()
    {
        // Logique pour s�lectionner une tour
        TowerPlacementManager.Instance.SelectTower(towerPrefab);
    }
}
