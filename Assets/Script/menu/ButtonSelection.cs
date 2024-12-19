using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonSelection : MonoBehaviour
{
    [Header("UI Settings")]
    public List<Button> buttons = new List<Button>(); // Liste des boutons dans l'interface
    public List<string> predefinedValues = new List<string>(); // Liste des valeurs associées

    void Start()
    {
        // Vérifie si le nombre de boutons correspond au nombre de valeurs
        if (buttons.Count != predefinedValues.Count)
        {
            Debug.LogError("Le nombre de boutons ne correspond pas au nombre de valeurs prédéfinies !");
            return;
        }

        // Associe chaque bouton à une valeur de la liste
        for (int i = 0; i < buttons.Count; i++)
        {
            int index = i; // Nécessaire pour capturer la bonne valeur dans une closure
            buttons[i].onClick.AddListener(() => SelectButton(index));
        }
    }

    void SelectButton(int index)
    {
        // Récupère la valeur associée au bouton
        string selectedValue = predefinedValues[index];
        Debug.Log($"Bouton {index + 1} sélectionné, valeur : {selectedValue}");

        // Faites ici ce que vous voulez avec `selectedValue`
    }
}
