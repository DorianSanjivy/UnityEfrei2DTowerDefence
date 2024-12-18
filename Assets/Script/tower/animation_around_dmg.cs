using UnityEngine;

public class RotateEffect : MonoBehaviour
{
    public float rotationSpeed = 200f; // Vitesse de rotation

    void Update()
    {
        // Faire tourner l'objet autour de son pivot local
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
