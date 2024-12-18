using UnityEngine;
using System;

[CreateAssetMenu(fileName = "NewWave", menuName = "Wave System/Wave", order = 1)]
public class WaveData : ScriptableObject
{
    public WaveStep[] waveSteps; // Les ennemis à générer dans cette vague
    //public float spawnInterval = 2f;  // Intervalle entre les spawns des ennemis
}

[Serializable]
public class WaveStep
{
    public GameObject enemyPrefab;
    public float spawnInterval;
}
