using UnityEngine;

public class Projectile_zone : MonoBehaviour
{
    public float speed = 5f;            // Vitesse du projectile
    public int damage = 50;             // Dégâts infligés
    public float explosionRadius = 2f;  // Rayon des dégâts de zone
    public GameObject explosionEffect;  // Effet visuel pour l'explosion (facultatif)

    private GameObject target;          // Cible de la tour

    public void SetTarget(GameObject newTarget)
    {
        target = newTarget;
        Debug.Log("trt : " + target.name);
    }

    void Update()
    {
        if (target != null)
        {
            // Déplacer le projectile vers la cible
        Vector3 direction = (target.transform.position - transform.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            //Debug.Log("Projectile se déplace vers : " + target.name);
            // Vérifie si le projectile atteint la cible
            if (Vector3.Distance(transform.position, target.transform.position) < 0.1f)
        {
            Explode();
        }
        }

        
    }

    private void Explode()
    {
        

        // Dégâts de zone : détecter tous les ennemis dans le rayon
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D collider in colliders)
        {
            Animal enemyHealth = collider.GetComponent<Animal>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
                Debug.Log("took damage"+ damage);
            }
        }

        // Détruire le projectile après l'explosion
        Destroy(gameObject);
    }

    // Visualiser le rayon d'explosion dans l'éditeur
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
