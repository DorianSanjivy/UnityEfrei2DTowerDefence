using UnityEngine;

public class ObjectShake : MonoBehaviour
{
    public float shakeAmount = 0.1f;  // Amplitude du tremblement (plus la valeur est grande, plus le tremblement est fort)
    public float shakeSpeed = 1.0f;   // Vitesse du tremblement (plus rapide pour des tremblements plus rapides)
    public float shakeDuration = 1.0f; // Durée du tremblement avant que l'objet ne revienne à sa position initiale

    private Vector3 originalPosition;
    private float shakeTimer;

    void Start()
    {
        // Sauvegarder la position initiale de l'objet
        originalPosition = transform.position;
    }

    void Update()
    {
        // Si un tremblement est en cours (durée non expirée)
        if (shakeTimer > 0)
        {
            // Appliquer le tremblement (déplacement aléatoire sur les axes X, Y, Z)
            float shakeX = Random.Range(-shakeAmount, shakeAmount);
            float shakeY = Random.Range(-shakeAmount, shakeAmount);
            float shakeZ = Random.Range(-shakeAmount, shakeAmount);

            // Modifier la position de l'objet pour simuler le tremblement
            transform.position = originalPosition + new Vector3(shakeX, shakeY, shakeZ);

            // Réduire le timer pour que le tremblement cesse après un certain temps
            shakeTimer -= Time.deltaTime;
        }
        else
        {
            // Si la durée du tremblement est terminée, ramener l'objet à sa position initiale
            transform.position = originalPosition;
        }
    }

    // Méthode publique pour démarrer le tremblement
    public void Shake()
    {
        shakeTimer = shakeDuration;  // Réinitialiser le timer de tremblement
    }
}
