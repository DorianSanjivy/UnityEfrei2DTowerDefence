using System.Collections.Generic;
using UnityEngine;

public class TowerGrid : MonoBehaviour
{
    public GameObject[] towerPrefabs; // Array to hold different tower prefabs
    public GameObject[] roadPrefabs; // Array to hold different road prefabs
    private string[,] testArray;

    private int rows = 9;  // Number of rows
    private int cols = 16; // Number of columns

    private bool isPaused = false; // Tracks if the game is paused

    private Dictionary<Vector2Int, GameObject> placedTowers = new Dictionary<Vector2Int, GameObject>(); // Track placed towers
    private Dictionary<string, int> towerMapping = new Dictionary<string, int>
    {
        { "Canon1", 0 },
        { "Canon2", 1 },
        { "Canon3", 2 },
        { "Wind1", 3 },
        { "Wind2", 4 },
        { "Wind3", 5 },
        { "Zone1", 6 },
        { "Zone2", 7 },
        { "Zone3", 8 },
        { "Spring1", 9 },
        { "Spring2", 10 },
        { "Spring3", 11 },
        { "MaxHP1", 12 },
        { "MaxHP2", 13 },
        { "MaxHP3", 14 }
    };

    private Dictionary<string, int> roadMapping = new Dictionary<string, int>
    {
        { "RoadV", 0 },
        { "RoadH", 1 },
        { "RoadC1", 2 },
        { "RoadC2", 3 },
        { "RoadC3", 4 },
        { "RoadC4", 5 }
    };

    private string selectedTowerIndex = "Wind"; // Currently selected tower type (default: Wind)

    private bool activate = false;

    private ButtonSelection buttonSelection;

    void Start()
    {

        buttonSelection = FindObjectOfType<ButtonSelection>();

        // Initialize the grid
        testArray = new string[9, 16]
        {
            { "Barn", "Barn", "Barn", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" },
            { "Barn", "Barn", "Barn", "0", "0", "RoadC4", "RoadH", "RoadH", "RoadC1", "0", "0", "0", "0", "0", "0", "0" },
            { "Barn", "Barn", "Barn", "0", "0", "RoadV", "0", "0", "RoadV", "0", "0", "0", "0", "0", "0", "0" },
            { "0", "RoadV", "0", "0", "0", "RoadV", "0", "0", "RoadV", "0", "RoadC4", "RoadH", "RoadH", "RoadC1", "0", "0" },
            { "0", "RoadV", "0", "0", "0", "RoadV", "0", "0", "RoadV", "0", "RoadV", "0", "0", "RoadV", "0", "0" },
            { "0", "RoadV", "0", "0", "0", "RoadV", "0", "0", "RoadV", "0", "RoadV", "0", "0", "RoadV", "0", "0" },
            { "0", "RoadC3", "RoadH", "RoadH", "RoadH", "RoadC2", "0", "0", "RoadV", "0", "RoadV", "0", "0", "RoadC3", "RoadH", "RoadH" },
            { "0", "0", "0", "0", "0", "0", "0", "0", "RoadC3", "RoadH", "RoadC2", "0", "0", "0", "0", "0" },
            { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" }
        };

        // Spawn roads
        SpawnRoads();
        testArray.ToString2DDebugLog();
    }

    void SpawnRoads()
    {
        Vector2 gridStart = new Vector2(-9f, 4.25f); // Top-left corner of the grid
        Vector2 gridEnd = new Vector2(9f, -5.3f);  // Bottom-right corner of the grid
        float cellWidth = (gridEnd.x - gridStart.x) / (cols - 1);
        float cellHeight = (gridStart.y - gridEnd.y) / (rows - 1);

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                string cellType = testArray[y, x];

                if (roadMapping.ContainsKey(cellType))
                {
                    // Calculate the position of the tile
                    float posX = gridStart.x + (x * cellWidth);
                    float posY = gridStart.y - (y * cellHeight);
                    Vector2 position = new Vector2(posX, posY);

                    // Get the road prefab index and instantiate
                    int roadIndex = roadMapping[cellType];
                    Instantiate(roadPrefabs[roadIndex], position, Quaternion.identity);
                }
            }
        }
    }

    private bool previousActivateState = false;

    void Update()
    {

        // Debug
        if (Input.GetKeyDown(KeyCode.Alpha1)) selectedTowerIndex = "Wind"; // Wind Tower
        if (Input.GetKeyDown(KeyCode.Alpha2)) selectedTowerIndex = "Slow"; // Slow Tower
        if (Input.GetKeyDown(KeyCode.Alpha3)) selectedTowerIndex = "Zone"; // Zone Tower
        if (Input.GetKeyDown(KeyCode.Alpha4)) selectedTowerIndex = "Big"; // Big Tower
        if (Input.GetKeyDown(KeyCode.Alpha5)) selectedTowerIndex = "MaxHP"; // MaxHP Tower

        selectedTowerIndex = buttonSelection.selectedTower + "1";

        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            if (activate)
            {
                TryToCreateTower(selectedTowerIndex);
            }
        }

        if (Input.GetMouseButtonDown(1)) // Right mouse button
        {
            if (activate)
            {
                TryToDeleteTower();
            }
        }

        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            if (activate && selectedTowerIndex == "Upgrade1")
            {
                UpgradeTower();
            }
        }

        if (activate != previousActivateState)
        {
            PauseUnpauseGame();
            previousActivateState = activate;
        }
    }

    public void ToggleActivate(){
        activate = !activate;
    }

    public void TryToCreateTower(string LocalselectedTowerIndex){
        int[] tile = GetTileFromMousePosition();

        if (tile != null)
        {
            if (testArray.GetValueAt(tile[1], tile[0]) == "0")
            {
                if (tile[0] < 13){
                    int towerIndex = towerMapping[LocalselectedTowerIndex];
                    int towerCost = towerPrefabs[towerIndex].GetComponent<Tower>().cost;

                    if (GlobalVariables.playerMoney >= towerCost)
                    {
                        GlobalVariables.playerMoney -= towerCost;

                        // Change the cell value based on selected tower
                        testArray.ChangeAt(tile[1], tile[0], LocalselectedTowerIndex);
                        testArray.ToString2DDebugLog();
                        InstantiateTower(tile[0], tile[1]);
                        SoundManager.Play("Construct");
                    }
                    else 
                    {
                        SoundManager.Play("CantBuy");
                    }
                }
            }
            else 
            {
                SoundManager.Play("CantBuy");
            }
        }
    }

    public void TryToDeleteTower(){
        int[] tile = GetTileFromMousePosition();
        if (tile != null)
        {
            DestroyTower(tile[0], tile[1]);
            testArray.ChangeAt(tile[1], tile[0], "0");
            testArray.ToString2DDebugLog();
        }
    }

    public void TryToUpgradeTower()
    {
        //maybe later ??
    }

    public void PauseUnpauseGame(){
        // Toggle between paused and unpaused
        if (isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
        }
        else
        {
            Time.timeScale = 0;
            isPaused = true;
        }
    }

    void InstantiateTower(int tileX, int tileY)
    {
        // Grid properties
        Vector2 gridStart = new Vector2(-9f, 4.25f); // Top-left corner of the grid
        Vector2 gridEnd = new Vector2(9f, -5.3f);  // Bottom-right corner of the grid

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
            // Get the tower type from the testArray
            string towerType = testArray[tileY, tileX];
            Debug.Log(towerType);
            
            // Ensure the tower type is valid
            if (towerMapping.ContainsKey(towerType))
            {
                // Get the tower index and cost
                int towerIndex = towerMapping[towerType];
                int towerCost = towerPrefabs[towerIndex].GetComponent<Tower>().cost;

                // Refund half the cost
                int refundAmount = Mathf.FloorToInt(towerCost / 2);
                GlobalVariables.playerMoney += refundAmount;

                Debug.Log($"Tower destroyed at {tileX}, {tileY}. Refunded: {refundAmount}");

                // Destroy the tower object
                Destroy(placedTowers[tileKey]);

                // Remove the tower from the tracking dictionary
                placedTowers.Remove(tileKey);
            }
            else
            {
                Debug.LogWarning($"Invalid tower type at {tileX}, {tileY}: {towerType}");
            }
        }
        else
        {
            Debug.LogWarning($"No tower to destroy at X={tileX}, Y={tileY}");
        }
    }

    public void UpgradeTower()
    {
        int[] tile = GetTileFromMousePosition();

        if (tile != null)
        {
            // Get the tile key
            Vector2Int tileKey = new Vector2Int(tile[0], tile[1]);

            // Check if there's a tower at this position
            if (placedTowers.ContainsKey(tileKey))
            {
                // Get the tower type from the testArray
                string towerType = testArray[tile[1], tile[0]];

                // Check if the tower type ends with a number
                if (towerType.Length > 1 && char.IsDigit(towerType[^1]))
                {
                    // Parse the level from the tower type
                    int currentLevel = int.Parse(towerType[^1].ToString());

                    // Increment the level for the upgrade
                    int upgradedLevel = currentLevel + 1;

                    // Create the new tower type
                    string upgradedTowerType = towerType.Substring(0, towerType.Length - 1) + upgradedLevel;

                    int towerIndex = towerMapping[upgradedTowerType];
                    int towerCost = towerPrefabs[towerIndex].GetComponent<Tower>().cost;

                    // Check if the upgraded type is valid (exists in towerMapping)
                    if (towerMapping.ContainsKey(upgradedTowerType))
                    {
                        if (GlobalVariables.playerMoney >= towerCost){
                            TryToDeleteTower();
                            TryToCreateTower(upgradedTowerType);

                            Debug.Log($"Upgraded {towerType} to {upgradedTowerType} at ({tile[0]}, {tile[1]})");
                        }
                        else{
                            SoundManager.Play("CantBuy");
                        }
                    }
                    else
                    {
                        SoundManager.Play("CantBuy");
                        Debug.LogWarning($"Upgrade type {upgradedTowerType} does not exist in towerMapping.");
                    }
                }
                else
                {
                    Debug.LogWarning($"Tower type {towerType} does not have a valid level to upgrade.");
                }
            }
            else
            {
                Debug.LogWarning($"No tower to upgrade at X={tile[0]}, Y={tile[1]}");
            }
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
