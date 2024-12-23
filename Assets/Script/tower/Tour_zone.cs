using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tour_zone : MonoBehaviour
{
    public GameObject projectilePrefab; // Le prefab du projectile
    private float fireRate; // Temps entre chaque tir
    public Transform firePoint; // Point d'o� partent les projectiles
    public float rotationSpeed = 5f; // Vitesse de rotation vers la cible
    private Tower towerScript;
    private List<GameObject> enemiesInRange = new List<GameObject>(); // Liste des ennemis dans le rayon
    private float fireCooldown = 0f; // Temps restant avant le prochain tir

    private TowerSquash towerSquash;  // Référence au script TowerSquash
    private bool isSquashing = false;  // État pour savoir si la tour est en train de faire un squash

    void Start() 
    {
        towerScript = GetComponent<Tower>(); 
        fireRate = towerScript.rate;
        towerSquash = GetComponent<TowerSquash>(); // Récupère le script TowerSquash
    }

    void Update()
    {
        // Retirer les ennemis détruits de la liste
        enemiesInRange.RemoveAll(enemy => enemy == null);

        if (enemiesInRange.Count > 0)
        {
            // Cibler le premier ennemi dans la liste
            GameObject target = enemiesInRange[0];

            if (target != null)
            {
                // Si la tour n'est pas en train de faire un squash, on effectue le squash et le tir
                if (!isSquashing && fireCooldown <= 0f)
                {
                    StartCoroutine(PerformSquashAndShoot(target)); // Démarre la coroutine de squash et de tir
                    fireCooldown = fireRate; // Réinitialise le cooldown
                }
            }
        }

        // Réduire le cooldown
        if (fireCooldown > 0f)
            fireCooldown -= Time.deltaTime;
    }

    private IEnumerator PerformSquashAndShoot(GameObject target)
    {
        isSquashing = true; // La tour commence le squash

        // Appliquer l'effet de squash pendant un certain temps (par exemple, 0.5 secondes)
        float squashDuration = 0.5f;
        float elapsedTime = 0f;
        float squashIntensity = 0.2f;
        
        // Sauvegarde la position et l'échelle d'origine de la tour
        Vector3 originalScale = transform.localScale;
        Vector3 originalPosition = transform.position;

        // Applique le squash pendant la durée
        while (elapsedTime < squashDuration)
        {
            float squashAmount = Mathf.Sin(elapsedTime * Mathf.PI / squashDuration) * squashIntensity;
            float newYScale = originalScale.y - squashAmount;
            transform.localScale = new Vector3(originalScale.x, newYScale, originalScale.z);
            
            // Déplace la tour pour compenser l'effet de squash
            float heightDifference = originalScale.y - newYScale;
            transform.position = new Vector3(originalPosition.x, originalPosition.y - heightDifference * 0.5f, originalPosition.z);
            
            elapsedTime += Time.deltaTime;
            yield return null; // Attendre une frame avant de continuer
        }

        // Une fois le squash terminé, la tour retrouve sa taille originale
        transform.localScale = originalScale;
        transform.position = originalPosition;

        // Appeler la fonction pour tirer le projectile
        Shoot(target);

        // Terminer le squash
        isSquashing = false;
    }

    private void Shoot(GameObject target)
    {
        if (projectilePrefab != null && firePoint != null)
        {
            // Créer le projectile
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

            // Donner une direction au projectile
            Projectile_zone projScript = projectile.GetComponent<Projectile_zone>();
            projScript.SetDamage(towerScript.damage);
            if (projScript != null && target != null)
            {
                projScript.SetTarget(target);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            print("detected");
            Debug.Log("Trigger détecté avec : " + other.gameObject.name);
            enemiesInRange.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(other.gameObject);
        }
    }
}
