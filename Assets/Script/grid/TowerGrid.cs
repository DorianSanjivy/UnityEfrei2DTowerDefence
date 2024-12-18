using System.Collections.Generic;
using UnityEngine;

public class TowerGrid : MonoBehaviour
{
    public GameObject[] towerPrefabs; // Array to hold different tower prefabs
    private string[,] testArray;

    private int rows = 9;  // Number of rows
    private int cols = 16; // Number of columns

    private Dictionary<Vector2Int, GameObject> placedTowers = new Dictionary<Vector2Int, GameObject>(); // Track placed towers
    private Dictionary<string, int> towerMapping = new Dictionary<string, int>
    {
        { "Wind", 0 },
        { "Slow", 1 },
        { "Zone", 2 },
        { "Big", 3 },
        { "MaxHP", 4 }
    };

    void Start()
    {
        // Initialize the grid
        testArray = new string[9, 16]
        {
            { "Barn", "Barn", "Barn", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" },
            { "Barn", "Barn", "Barn", "0", "0", "Road", "Road", "Road", "Road", "0", "0", "0", "0", "0", "0", "0" },
            { "Barn", "Barn", "Barn", "0", "0", "Road", "0", "0", "Road", "0", "0", "0", "0", "0", "0", "0" },
            { "0", "Road", "0", "0", "0", "Road", "0", "0", "Road", "0", "Road", "Road", "Road", "Road", "0", "0" },
            { "0", "Road", "0", "0", "0", "Road", "0", "0", "Road", "0", "Road", "0", "0", "Road", "0", "0" },
            { "0", "Road", "0", "0", "0", "Road", "0", "0", "Road", "0", "Road", "0", "0", "Road", "0", "0" },
            { "0", "Road", "Road", "Road", "Road", "Road", "0", "0", "Road", "0", "Road", "0", "0", "Road", "Road", "Road" },
            { "0", "0", "0", "0", "0", "0", "0", "0", "Road", "Road", "Road", "0", "0", "0", "0", "0" },
            { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" }
        };

        // Example initial placement
        testArray.ChangeAt(3, 6, "Wind");
        InstantiateTower(6, 3);
        testArray.ChangeAt(8, 15, "Slow");
        InstantiateTower(15, 8);
        testArray.ChangeAt(5, 12, "Zone");
        InstantiateTower(12, 5);

        testArray.ToString2DDebugLog();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            int[] tile = GetTileFromMousePosition();
            if (tile != null)
            {
                if (testArray.GetValueAt(tile[1], tile[0]) == "0")
                {
                    if (GlobalVariables.playerMoney >= 1)
                    {
                        GlobalVariables.playerMoney -= 1;
                        testArray.ChangeAt(tile[1], tile[0], "Wind"); // Example: Wind tower
                        InstantiateTower(tile[0], tile[1]);
                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(1)) // Right mouse button
        {
            int[] tile = GetTileFromMousePosition();
            if (tile != null)
            {
                testArray.ChangeAt(tile[1], tile[0], "0");
                testArray.ToString2DDebugLog();
                DestroyTower(tile[0], tile[1]);
            }
        }
    }

    void InstantiateTower(int tileX, int tileY)
    {
        // Grid properties
        Vector2 gridStart = new Vector2(-9f, 4.25f); // Top-left corner of the grid
        Vector2 gridEnd = new Vector2(9.1f, -5.3f);  // Bottom-right corner of the grid

        // Ensure the indices are valid
        if (tileX < 0 || tileX >= cols || tileY < 0 || tileY >= rows)
        {
            Debug.LogError($"Invalid tile indices: X={tileX}, Y={tileY}");
            return;
        }

        // Calculate the tile size
        float cellWidth = (gridEnd.x - gridStart.x) / (cols - 1);
        float cellHeight = (gridStart.y - gridEnd.y) / (rows - 1);

        // Calculate the position of the tile
        float posX = gridStart.x + (tileX * cellWidth);
        float posY = gridStart.y - (tileY * cellHeight);
        Vector2 position = new Vector2(posX, posY);

        // Get the cell type
        string cellType = testArray[tileY, tileX];

        // Check if the cell type is mapped to a tower
        if (towerMapping.ContainsKey(cellType))
        {
            int towerIndex = towerMapping[cellType];

            // Instantiate the appropriate tower prefab
            GameObject tower = Instantiate(towerPrefabs[towerIndex], position, Quaternion.identity);

            // Add the tower to the tracking dictionary
            Vector2Int tileKey = new Vector2Int(tileX, tileY);
            placedTowers[tileKey] = tower;
        }
        else
        {
            Debug.LogWarning($"No tower mapping found for cell type: {cellType}");
        }
    }

    void DestroyTower(int tileX, int tileY)
    {
        // Get the tile key
        Vector2Int tileKey = new Vector2Int(tileX, tileY);

        // Check if there's a tower at this position
        if (placedTowers.ContainsKey(tileKey))
        {
            // Destroy the tower
            Destroy(placedTowers[tileKey]);

            // Remove the tower from the tracking dictionary
            placedTowers.Remove(tileKey);
        }
        else
        {
            Debug.LogWarning($"No tower to destroy at X={tileX}, Y={tileY}");
        }
    }

    public int[] GetTileFromMousePosition()
    {
        // Convert mouse position to world point
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Grid properties
        Vector2 gridStart = new Vector2(-9.55f, 5.4f);  // Top-left corner of the grid
        Vector2 gridEnd = new Vector2(8.4f, -4.2f);     // Bottom-right corner of the grid

        // Calculate tile size
        float cellWidth = (gridEnd.x - gridStart.x) / (cols - 1);
        float cellHeight = (gridStart.y - gridEnd.y) / (rows - 1);

        // Calculate the tile indices
        int tileX = Mathf.Clamp(Mathf.FloorToInt((mousePosition.x - gridStart.x) / cellWidth), 0, cols - 1);
        int tileY = Mathf.Clamp(Mathf.FloorToInt((gridStart.y - mousePosition.y) / cellHeight), 0, rows - 1);

        // Return the tile indices
        return new int[] { tileX, tileY };
    }
}
