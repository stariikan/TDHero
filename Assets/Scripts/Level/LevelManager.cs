using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public string levelName;
    public ActiveCells activeCells;
}

[System.Serializable]
public class ActiveCells
{
    public List<string> G; // Green cells
    public List<string> R; // Red cells
}

public class LevelManager : MonoBehaviour
{
    [Header("JSON File Path")]
    public string jsonFileName = "level1.json";

    [Header("Cell References")]
    public Dictionary<string, GameObject> cellDictionary = new Dictionary<string, GameObject>();
    private void Awake()
    {
        foreach (GameObject cell in FindObjectsOfType<GameObject>())
        {
            if (cell.name.Contains("G:") || cell.name.Contains("R:"))
            {
                cellDictionary[cell.name] = cell;
                cell.SetActive(false); // Deactivate all cells initially
            }
        }
    }

    private void Start()
    {
        LoadLevelData();
    }

    public void LoadLevelData()
    {
        // Load the JSON file
        string jsonFilePath = Path.Combine(Application.streamingAssetsPath, jsonFileName);
        if (File.Exists(jsonFilePath))
        {
            string jsonData = File.ReadAllText(jsonFilePath);
            LevelData levelData = JsonUtility.FromJson<LevelData>(jsonData);

            // Activate cells based on JSON data
            ActivateCells(levelData.activeCells);
        }
        else
        {
            Debug.LogError($"JSON file not found at: {jsonFilePath}");
        }
    }

    private void ActivateCells(ActiveCells activeCells)
    {
        // Activate "G" cells
        foreach (string cellName in activeCells.G)
        {
            if (cellDictionary.TryGetValue(cellName, out GameObject cell))
            {
                cell.SetActive(true);
            }
            else
            {
                Debug.LogWarning($"Cell {cellName} not found in the dictionary.");
            }
        }

        // Activate "R" cells
        foreach (string cellName in activeCells.R)
        {
            if (cellDictionary.TryGetValue(cellName, out GameObject cell))
            {
                cell.SetActive(true);
            }
            else
            {
                Debug.LogWarning($"Cell {cellName} not found in the dictionary.");
            }
        }
    }
}
