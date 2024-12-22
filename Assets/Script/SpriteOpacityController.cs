using UnityEngine;

public class SpriteOpacityController : MonoBehaviour
{
    // Reference to the black sprite
    private SpriteRenderer spriteRenderer;

    // Update interval for performance optimization
    [SerializeField] private float updateInterval = 0.1f; // Update every 0.1 seconds
    private float updateTimer;

    private void Start()
    {
        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("No SpriteRenderer found on this GameObject!");
        }

        Color spriteColor = spriteRenderer.color;
        spriteColor.a = 0;
        spriteRenderer.color = spriteColor;
    }

    private void Update()
    {
        if (spriteRenderer == null)
            return;

        // Update the sprite opacity based on GlobalVariables.grangeCurrentHealth
        updateTimer += Time.deltaTime;
        if (updateTimer >= updateInterval)
        {
            updateTimer = 0f;

            // Example: Map health (assumed to range from 0 to 100) to sprite opacity (range 0 to 1)
            float health = Mathf.Clamp(GlobalVariables.grangeCurrentHealth, 0, 100); // Ensure health stays within bounds
            float alpha = Mathf.Lerp(0.8f, 0f, health / 100f); // Lower health, higher opacity

            // Modify the sprite's color alpha
            Color spriteColor = spriteRenderer.color;
            spriteColor.a = alpha;
            spriteRenderer.color = spriteColor;
        }
    }
}
