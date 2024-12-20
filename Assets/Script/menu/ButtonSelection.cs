using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonSelection : MonoBehaviour
{
    [Header("UI Settings")]
    public List<Button> buttons = new List<Button>(); // Liste des boutons dans l'interface
    public List<string> predefinedValues = new List<string>(); // Liste des valeurs associ�es
    public string selectedTower = "Wind";

    void Start()
    {
        // V�rifie si le nombre de boutons correspond au nombre de valeurs
        if (buttons.Count != predefinedValues.Count)
        {
            Debug.LogError("Le nombre de boutons ne correspond pas au nombre de valeurs pr�d�finies !");
            return;
        }

        // Associe chaque bouton � une valeur de la liste
        for (int i = 0; i < buttons.Count; i++)
        {
            int index = i; // N�cessaire pour capturer la bonne valeur dans une closure
            buttons[i].onClick.AddListener(() => SelectButton(index));
        }
    }

    void SelectButton(int index)
    {
        // R�cup�re la valeur associ�e au bouton
        selectedTower = predefinedValues[index];
        Debug.Log($"Bouton {index + 1} s�lectionn�, valeur : {selectedTower}");
    }
}
