using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Array of enemy prefabs
    public PathManager pathManager;
    public float spawnInterval = 2f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 0f, spawnInterval);
    }

    private void SpawnEnemy()
    {
        if (enemyPrefabs.Length == 0 || pathManager.nodes.Length == 0) return; // Safety check

        // Randomly select an enemy prefab from the array
        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        // Spawn the selected enemy at the first node
        GameObject enemy = Instantiate(enemyPrefab, pathManager.nodes[0].position, Quaternion.identity);

        // Set the path for the spawned enemy
        if (enemy.TryGetComponent(out Animal animal))
        {
            animal.SetPath(pathManager.nodes);
        }
    }
}

