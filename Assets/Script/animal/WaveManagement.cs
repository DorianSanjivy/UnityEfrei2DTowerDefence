using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour
{
    public List<WaveData> waves; // Liste des vagues à gérer
    public PathManager pathManager;
    public float timeBetweenWaves = 5f; // Temps entre chaque vague

    private int currentWaveIndex = 0;
    private List<GameObject> activeEnemies = new List<GameObject>(); // Suivi des ennemis actifs
    private bool stopSpawning = false; // Permet d'arrêter le spawn des vagues

    private void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        while (currentWaveIndex < waves.Count && !stopSpawning)
        {
            WaveData currentWave = waves[currentWaveIndex];
            Debug.Log($"Début de la vague {currentWaveIndex + 1}");

            // Générer les ennemis pour la vague actuelle
            yield return StartCoroutine(SpawnWave(currentWave));

            // Attendre que tous les ennemis soient détruits avant de passer à la vague suivante
            yield return new WaitUntil(() => activeEnemies.Count == 0);

            Debug.Log($"Vague {currentWaveIndex + 1} terminée !");
            currentWaveIndex++;

            // Temps d'attente avant la prochaine vague
            if (currentWaveIndex < waves.Count && !stopSpawning)
            {
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        }

        if (stopSpawning)
        {
            Debug.Log("Le système de vagues a été arrêté !");
        }
        else
        {
            Debug.Log("Toutes les vagues ont été générées !");
        }
    }

    private IEnumerator SpawnWave(WaveData wave)
    {
        foreach (WaveStep step in wave.waveSteps)
        {
            if (stopSpawning)
                yield break; // Arrêter le spawn si le système est stoppé

            if (pathManager.nodes.Length > 0)
            {
                // Instancier l'ennemi au premier nœud
                GameObject enemy = Instantiate(step.enemyPrefab, pathManager.nodes[0].position, Quaternion.identity);
                activeEnemies.Add(enemy); // Ajouter à la liste des ennemis actifs

                // Assigner le chemin à l'ennemi
                if (enemy.TryGetComponent(out Animal animal))
                {
                    animal.SetPath(pathManager.nodes);
                }
            }

            // Attendre l'intervalle de spawn avant de générer le prochain ennemi
            yield return new WaitForSeconds(step.spawnInterval);
        }
    }

    private void Update()
    {
        // Nettoyer la liste des ennemis actifs (supprimer ceux qui sont détruits)
        activeEnemies.RemoveAll(enemy => enemy == null);
    }

    public void StopSpawning()
    {
        stopSpawning = true;

        // Supprimer tous les ennemis actifs
        foreach (GameObject enemy in activeEnemies)
        {
            if (enemy != null)
            {
                Destroy(enemy);
            }
        }
        activeEnemies.Clear(); // Vider la liste
    }
}
