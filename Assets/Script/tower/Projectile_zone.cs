using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_zone : MonoBehaviour
{
    public float speed = 5f;            // Vitesse du projectile
    private int damage;                 // Dégâts infligés
    public float explosionRadius = 2f;  // Rayon des dégâts de zone
    public GameObject explosionEffect;  // Effet visuel pour l'explosion (optionnel)

    private SpriteRenderer spriteRenderer;  // Référence au SpriteRenderer du projectile
    public Sprite[] explosionSprites;      // Tableau des sprites d'explosion
    public float explosionFrameRate = 0.1f; // Temps entre chaque frame de l'animation d'explosion

    private GameObject target;          // Cible de la tour
    private Vector3 startPos;           // Position de départ
    private Vector3 targetPos;          // Position de la cible
    private float startTime;            // Temps de départ du projectile
    private float travelTime;           // Temps total de vol

    public float curveHeight = 2f;      // Hauteur du sommet de la courbe
    private bool isExploding = false;   // Indique si l'explosion est en cours

    public void SetDamage(int newDamage) { damage = newDamage; }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetTarget(GameObject newTarget)
    {
        target = newTarget;
        targetPos = target.transform.position;
        startPos = transform.position;

        // Calculer le temps de vol en fonction de la distance et de la vitesse
        travelTime = Vector3.Distance(startPos, targetPos) / speed;

        startTime = Time.time;  // Enregistrer le moment où le tir commence
    }

    void Update()
    {
        // Si l'explosion est en cours, ne pas exécuter la logique de mouvement
        if (isExploding) return;

        if (target != null)
        {
            float timeElapsed = Time.time - startTime;
            float percentageComplete = timeElapsed / travelTime;

            if (percentageComplete < 1)
            {
                // Calculer la position courbée
                Vector3 midPoint = (startPos + targetPos) / 2;
                midPoint.y += curveHeight;
                transform.position = CalculateParabolicCurve(startPos, midPoint, targetPos, percentageComplete);
            }
            else
            {
                Explode();
            }
        }
        else
        {
            Destroy(gameObject); // Si la cible n'existe plus, détruire le projectile
        }
    }

    private Vector3 CalculateParabolicCurve(Vector3 start, Vector3 mid, Vector3 end, float t)
    {
        float u = 1 - t;
        return u * u * start + 2 * u * t * mid + t * t * end;
    }

    private void Explode()
    {
        // Arrêter toute logique de mise à jour
        isExploding = true;

        // Appliquer immédiatement les dégâts
        ApplyExplosionDamage();

        // Lancer l'animation d'explosion
        StartCoroutine(PlayExplosionAnimation());
    }

    private IEnumerator PlayExplosionAnimation()
    {
        // Afficher les sprites d'explosion
        for (int i = 0; i < explosionSprites.Length; i++)
        {
            spriteRenderer.sprite = explosionSprites[i];
            yield return new WaitForSeconds(explosionFrameRate);
        }

        // Détruire le projectile après l'animation
        Destroy(gameObject);
    }

    private void ApplyExplosionDamage()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D collider in colliders)
        {
            Animal enemyHealth = collider.GetComponent<Animal>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }

        // Optionnel : Ajouter un effet visuel (comme des particules)
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }
    }

    // Visualiser le rayon d'explosion dans l'éditeur
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
