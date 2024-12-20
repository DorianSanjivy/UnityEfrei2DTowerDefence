using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End_Game : MonoBehaviour
{
    public GameObject bossPrefab; // Le prefab du boss à assigner dans l'inspecteur
    public Transform bossSpawnPoint; // Le point où le boss apparaîtra
    private bool bossSpawned = false; // Pour éviter que le boss ne soit spawn plusieurs fois

    void Start()
    {
    }

    void Update()
    {
        // Vérifier si la vie de la grange atteint zéro
        if (GlobalVariables.grangeCurrentHealth <= 0 && !bossSpawned)
        {
            StopEnemySpawn(); // Arrêter le système de spawn des ennemis
            RemoveAllMobs();  // Supprimer tous les ennemis actifs dans la scène
            SpawnBoss();      // Faire apparaître le boss

            // Marquer que le boss a déjà été spawn pour éviter de le faire plusieurs fois
            bossSpawned = true;
        }
    }

    /// <summary>
    /// Arrête le système de spawn des ennemis en utilisant le WaveManager.
    /// </summary>
    void StopEnemySpawn()
    {
        // Trouver le WaveManager dans la scène
        WaveManager waveManager = FindObjectOfType<WaveManager>();
        if (waveManager != null)
        {
            waveManager.StopSpawning(); // Appeler la méthode pour arrêter les vagues
        }
        else
        {
            Debug.LogWarning("WaveManager introuvable dans la scène !");
        }
    }

    /// <summary>
    /// Supprime tous les ennemis actifs dans la scène.
    /// </summary>
    void RemoveAllMobs()
    {
        // Trouver tous les objets ayant le tag "Mob"
        GameObject[] mobs = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject mob in mobs)
        {
            Destroy(mob); // Détruire chaque mob trouvé
        }
    }

    /// <summary>
    /// Fait apparaître un boss à un point spécifique dans la scène.
    /// </summary>
    void SpawnBoss()
    {
        if (bossPrefab != null && bossSpawnPoint != null)
        {
            // Instancier le boss au point défini dans l'inspecteur
            Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("BossPrefab ou BossSpawnPoint n'est pas défini dans l'inspecteur !");
        }
    }
}
