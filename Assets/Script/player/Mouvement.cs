using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouvement : MonoBehaviour
{
    public float maxSpeed = 5f; // Vitesse maximale
    public float acceleration = 10f; // Accélération
    public float deceleration = 15f; // Décélération

    private Vector3 currentVelocity; // Vitesse actuelle

    void Update()
    {
        // Récupère les entrées du clavier
        float horizontalInput = Input.GetAxisRaw("Horizontal"); // Entrée brute (sans interpolation)
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Détermine la direction souhaitée
        Vector3 targetDirection = new Vector3(horizontalInput, verticalInput, 0f).normalized;

        // Si une touche est pressée, accélère vers la direction cible
        if (targetDirection.magnitude > 0)
        {
            currentVelocity = Vector3.MoveTowards(currentVelocity, targetDirection * maxSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            // Si aucune touche n'est pressée, applique une décélération pour ralentir progressivement
            currentVelocity = Vector3.MoveTowards(currentVelocity, Vector3.zero, deceleration * Time.deltaTime);
        }

        // Déplace l'objet en fonction de la vitesse actuelle
        transform.position += currentVelocity * Time.deltaTime;
    }
}