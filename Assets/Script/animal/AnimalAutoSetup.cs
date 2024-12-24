using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;

public class AnimalAutoSetup : MonoBehaviour
{

    public GameObject[] animalPrefabs;

    private Dictionary<string, int> animalMapping = new Dictionary<string, int>
    {
        { "Cochon", 0 },
        { "CochonALT", 1 },
        { "Minipiou", 2 },
        { "Multipiou", 3 },
        { "Poulet", 4 },
        { "PouletALT", 5 },
        { "Vache", 6 },
        { "VacheALT", 7 }
    };

    [SerializeField]
    private StatTab animalStats;

    // Start is called before the first frame update
    void Start()
    {
        // Loop through the grid
        for (int y = 0; y < animalStats.array.GridSize.y; y++)
        {
            string cellValue = animalStats.array.GetCell(0, y);
            // Check if the cellValue exists in the towerMapping
            if (animalMapping.ContainsKey(cellValue))
            {
                int animalIndex = animalMapping[cellValue];
                GameObject animalPrefab = animalPrefabs[animalIndex];

                // Modify the stats of the tower prefab
                Animal animalComponent = animalPrefab.GetComponent<Animal>();
                if (animalComponent != null)
                {
                    UpdateAnimalStats(animalComponent, y);
                }
                else
                {
                    Debug.LogWarning($"Tower prefab for {cellValue} does not have a Tower component.");
                }
            }
        }
    
    }

    void UpdateAnimalStats(Animal animal, int y)
    {
        animal.speed = float.Parse(animalStats.array.GetCell(1,y), CultureInfo.InvariantCulture);
        animal.health = int.Parse(animalStats.array.GetCell(2,y));
        animal.damage = int.Parse(animalStats.array.GetCell(3,y));
        animal.moneyDrop = int.Parse(animalStats.array.GetCell(4,y));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
