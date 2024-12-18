using UnityEngine;

public class DamageTest : MonoBehaviour
{
    void Update()
    {
        // Réduire la vie avec la touche "Espace"
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GlobalVariables.grangeCurrentHealth -= 10;

            // Clamp pour éviter que la santé devienne négative
            GlobalVariables.grangeCurrentHealth = Mathf.Clamp(GlobalVariables.grangeCurrentHealth, 0, GlobalVariables.grangeMaxHealth);
        }
    }
}
