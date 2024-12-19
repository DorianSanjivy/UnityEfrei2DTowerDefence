using UnityEngine;

public class HelixRotation : MonoBehaviour
{
    // Vitesse de rotation en degrés par seconde
    public float rotationSpeed = 360f;

    // Update est appelé à chaque image
    void Update()
    {
        // Tourner l'objet autour de son axe Z
        transform.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime);
    }
}
