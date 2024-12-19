using UnityEngine;

public class TowerSquash : MonoBehaviour
{
    public float squashIntensity = 0.1f;  // Intensité du squash (0.0 - 1.0)
    public float squashSpeed = 1.0f;     // Vitesse du squash
    private Vector3 originalScale;
    private Vector3 originalPosition;

    void Start()
    {
        // Sauvegarder l'échelle et la position originale de l'objet Tower
        originalScale = transform.localScale;
        originalPosition = transform.position;
    }

    void Update()
    {
        // Calculer l'effet de squash en utilisant un sinus pour une variation douce
        float squashAmount = Mathf.Sin(Time.time * squashSpeed) * squashIntensity;

        // Appliquer le squash uniquement sur l'axe Y
        float newYScale = originalScale.y - squashAmount;

        // Appliquer l'échelle modifiée
        transform.localScale = new Vector3(originalScale.x, newYScale, originalScale.z);

        // Calculer combien le haut de la tour descend (par rapport à l'échelle modifiée)
        float heightDifference = originalScale.y - newYScale;

        // Déplacer l'objet pour que la base reste fixe, en ajustant la position Y
        transform.position = new Vector3(originalPosition.x, originalPosition.y - heightDifference * 0.5f, originalPosition.z);
    }
}
