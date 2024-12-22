using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VignetteEffectController : MonoBehaviour
{
    // Reference to the post-processing volume
    private Volume postProcessVolume;
    private Vignette vignette;
    private ColorAdjustments colorAdjustments;

    [SerializeField] private float updateInterval = 0.1f; // Update every 0.1 seconds
    private float updateTimer;

    private void Start()
    {
        // Get the Volume component
        postProcessVolume = GetComponent<Volume>();

        if (postProcessVolume == null)
        {
            Debug.LogError("Post-process volume not found!");
            return;
        }

        // Try to get the Vignette and Color Adjustments effects
        if (!postProcessVolume.profile.TryGet<Vignette>(out vignette))
        {
            Debug.LogError("Vignette effect is not found in the post-processing volume!");
        }

        if (!postProcessVolume.profile.TryGet<ColorAdjustments>(out colorAdjustments))
        {
            Debug.LogError("Color Adjustments effect is not found in the post-processing volume!");
        }
    }

    private void Update()
    {
        if (vignette == null || colorAdjustments == null)
            return;

        // Update the effects based on GlobalVariables.grangeCurrentHealth
        updateTimer += Time.deltaTime;
        if (updateTimer >= updateInterval)
        {
            updateTimer = 0f;

            // Example: Map health (assumed to range from 0 to 100) to vignette intensity and saturation
            float health = Mathf.Clamp(GlobalVariables.grangeCurrentHealth, 0, 100); // Ensure health stays within bounds

            // Adjust vignette intensity (0.3 at low health, 0 at full health)
            vignette.intensity.value = Mathf.Lerp(0.3f, 0f, health / 100f);

            // Adjust grayscale saturation (-100 at low health, 0 at full health)
            colorAdjustments.saturation.value = Mathf.Lerp(-18f, 0f, health / 100f);
        }
    }
}
