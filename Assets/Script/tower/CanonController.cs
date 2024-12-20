using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public Transform anchor; // Point autour duquel le canon tourne
    public float rotationSpeed = 720f; // Vitesse de rotation en degrés par seconde

    private Transform target; // Cible actuelle du canon

    // Appelé pour définir la cible que le canon doit viser
    public void AimAt(Transform enemyTarget)
    {
        target = enemyTarget;
        if (target != null)
        {
            StopCoroutine("RotateTowardsTarget");
            StartCoroutine(RotateTowardsTarget());
        }
    }

    private IEnumerator RotateTowardsTarget()
    {
        while (target != null)
        {
            // Calculer la direction vers la cible
            Vector3 direction = target.position - anchor.position;
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Calculer l'angle actuel du canon
            float currentAngle = transform.eulerAngles.z;

            // Effectuer une rotation douce vers l'angle cible
            float newAngle = Mathf.LerpAngle(currentAngle, targetAngle, Time.deltaTime * rotationSpeed);

            // Appliquer la rotation au canon
            transform.eulerAngles = new Vector3(0, 0, newAngle);

            // Vérifier si le canon est proche de la bonne direction
            if (Mathf.Abs(Mathf.DeltaAngle(currentAngle, targetAngle)) < 0.1f)
                break;

            yield return null;
        }
    }
}
