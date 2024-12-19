using UnityEngine;

public class CannonController : MonoBehaviour
{
    public Transform rafaleTower;   // Référence au Transform de l'enfant "rafale_tower"
    public GameObject arrow;        // Référence au GameObject de la flèche

    void Update()
    {
        if (arrow != null)
        {
            // Récupérer le script Projectile attaché au GameObject arrow
            Projectile arrowScript = arrow.GetComponent<Projectile>();

            if (arrowScript != null)
            {
                // Obtenir la cible de la flèche
                GameObject target = arrowScript.GetTarget();
                if (target != null)
                {
                    // Calculer la direction de la flèche vers sa cible
                    Vector2 direction = (target.transform.position - transform.position).normalized;

                    // Calculer l'angle de rotation en degrés
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                    // Appliquer la rotation au Transform "rafale_tower"
                    rafaleTower.rotation = Quaternion.Euler(0, 0, angle);
                }
            }
        }
    }
}
