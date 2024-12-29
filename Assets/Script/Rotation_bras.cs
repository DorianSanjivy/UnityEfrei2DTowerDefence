using UnityEngine;

public class Rotation_bras : MonoBehaviour
{
    // Vitesse de rotation en degrés par seconde
    public float rotationSpeed = 1440f;

    // Booléen pour activer/désactiver la rotation
    private bool isRotating = false;

    // Angle accumulé depuis le début de la rotation
    private float currentRotationAngle = 0f;

    // Méthode pour activer la rotation
    public void StartRotation()
    {
        isRotating = true;
        currentRotationAngle = 0f; // Réinitialiser l'angle accumulé
    }

    // Update est appelé à chaque frame
    void Update()
    {
        // Tourner uniquement si isRotating est vrai
        if (isRotating)
        {
            // Calculer la rotation pour cette frame
            float rotationThisFrame = rotationSpeed * Time.deltaTime;
            transform.Rotate(0f, 0f, rotationThisFrame);

            // Ajouter à l'angle accumulé
            currentRotationAngle += rotationThisFrame;

            // Arrêter la rotation après 360°
            if (currentRotationAngle >= 360f)
            {
                isRotating = false;
                currentRotationAngle = 0f; // Réinitialiser pour la prochaine rotation
            }
        }
    }
}

