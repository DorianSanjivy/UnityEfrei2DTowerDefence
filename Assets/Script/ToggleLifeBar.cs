using UnityEngine;

public class ToggleLifeBar : MonoBehaviour
{
    // Reference to the LifeBar GameObject
    public GameObject lifeBar;

    void Update()
    {
        // Check if the space key is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Toggle the active state of the LifeBar GameObject
            if (lifeBar != null)
            {
                lifeBar.SetActive(!lifeBar.activeSelf);
            }
            else
            {
                Debug.LogWarning("LifeBar GameObject is not assigned!");
            }
        }
    }
}
