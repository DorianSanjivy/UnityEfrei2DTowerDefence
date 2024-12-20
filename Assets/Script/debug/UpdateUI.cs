using UnityEngine;
using TMPro;

public class UpdateUI : MonoBehaviour
{
    // Reference to the two TextMeshPro components
    public TextMeshProUGUI LifeTxt;
    public TextMeshProUGUI MoneyTxt;

    void Start()
    {
        // Initialize text components
        UpdateText();
    }

    void Update()
    {
        // Continuously update if the global variable changes dynamically
        UpdateText();
    }

    void UpdateText()
    {
        string value = "Life: " + GlobalVariables.grangeCurrentHealth.ToString(); 
        LifeTxt.text = value;
        string value2 = GlobalVariables.playerMoney.ToString();
        MoneyTxt.text = value2;
    }
}
