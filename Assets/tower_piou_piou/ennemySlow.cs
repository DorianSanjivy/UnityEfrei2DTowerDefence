using UnityEngine;

public class ennemySlow : MonoBehaviour
{
    public float speed = 3f; // Vitesse normale de l'ennemi
    private float currentSpeed; // Vitesse actuelle après ralentissement
    private bool isSlowed = false;

    void Start()
    {
        currentSpeed = speed;
    }

    void Update()
    {
        // Déplacer l'ennemi vers l'avant (ou selon ton chemin)
        transform.Translate(Vector3.right * currentSpeed * Time.deltaTime);
    }

    public void ApplySlow(float slowFactor, float duration)
    {
        if (!isSlowed)
        {
            isSlowed = true;
            currentSpeed *= slowFactor; // Réduit la vitesse
            Debug.Log(gameObject.name + " est ralenti à " + currentSpeed);

            // Restaure la vitesse après la durée
            Invoke("ResetSpeed", duration);
        }
    }

    private void ResetSpeed()
    {
        currentSpeed = speed; // Réinitialise la vitesse normale
        isSlowed = false;
        Debug.Log(gameObject.name + " retrouve sa vitesse normale.");
    }
}
