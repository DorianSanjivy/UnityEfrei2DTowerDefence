using UnityEngine;
using TMPro;

public class MousePositionDisplay : MonoBehaviour
{
    // Reference to the TextMeshProUGUI element that will display the mouse position
    public TextMeshProUGUI positionText;

    void Update()
    {
        // Get the mouse position in screen coordinates
        Vector3 mousePosition = Input.mousePosition;
        
        // Update the TextMeshProUGUI with the current mouse position
        positionText.text = "X= " + mousePosition.x.ToString("F0") + ", Y= " + mousePosition.y.ToString("F0");
    }
}
