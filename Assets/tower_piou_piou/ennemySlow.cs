using UnityEngine;

public class ennemySlow : MonoBehaviour
{
    public float speed = 3f; // Vitesse normale de l'ennemi
    private float currentSpeed; // Vitesse actuelle apr�s ralentissement
    private bool isSlowed = false;

    void Start()
    {
        currentSpeed = speed;
    }

    void Update()
    {
        // D�placer l'ennemi vers l'avant (ou selon ton chemin)
        transform.Translate(Vector3.right * currentSpeed * Time.deltaTime);
    }

    public void ApplySlow(float slowFactor, float duration)
    {
        if (!isSlowed)
        {
            isSlowed = true;
            currentSpeed *= slowFactor; // R�duit la vitesse
            Debug.Log(gameObject.name + " est ralenti � " + currentSpeed);

            // Restaure la vitesse apr�s la dur�e
            Invoke("ResetSpeed", duration);
        }
    }

    private void ResetSpeed()
    {
        currentSpeed = speed; // R�initialise la vitesse normale
        isSlowed = false;
        Debug.Log(gameObject.name + " retrouve sa vitesse normale.");
    }
}
