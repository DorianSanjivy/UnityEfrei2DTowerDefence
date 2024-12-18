using UnityEngine;

public class HealthBarManager : MonoBehaviour
{
    public GameObject healthBar; // Référence à la barre de vie dynamique
    public GameObject healthBarBackground; // Référence à l'image de fond

    private Vector3 initialScale; // Échelle initiale de la barre de vie
    private Vector3 initialPosition; // Position initiale de la barre de vie

    void Start()
    {
        if (healthBar != null)
        {
            // Enregistrer l'échelle et la position initiale de la barre de vie
            initialScale = healthBar.transform.localScale;
            initialPosition = healthBar.transform.position;

            // Masquer la barre de vie dynamique au début
            healthBar.SetActive(false);
        }

        if (healthBarBackground != null)
        {
            // Masquer le fond au début
            healthBarBackground.SetActive(false);
        }
    }

    void Update()
    {
        if (healthBar == null || healthBarBackground == null) return;

        // Calculer la proportion de vie restante
        float healthPercentage = (float)GlobalVariables.grangeCurrentHealth / GlobalVariables.grangeMaxHealth;

        // Afficher la barre de vie et le fond si des dégâts ont été subis
        if (GlobalVariables.grangeCurrentHealth < GlobalVariables.grangeMaxHealth)
        {
            if (!healthBar.activeSelf)
            {
                healthBar.SetActive(true);
            }
            if (!healthBarBackground.activeSelf)
            {
                healthBarBackground.SetActive(true);
            }
        }

        // Redimensionner l'échelle de la barre de vie sur l'axe X
        healthBar.transform.localScale = new Vector3(initialScale.x * healthPercentage, initialScale.y, initialScale.z);

        // Ajuster la position pour que la barre rétrécisse à partir de la gauche
        healthBar.transform.position = new Vector3(
            initialPosition.x - (initialScale.x * (1 - healthPercentage) / 2),
            initialPosition.y,
            initialPosition.z
        );

        // Changer la couleur de la barre en fonction de la santé restante (optionnel)
        SpriteRenderer spriteRenderer = healthBar.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.Lerp(Color.red, Color.green, healthPercentage);
        }
    }
}
