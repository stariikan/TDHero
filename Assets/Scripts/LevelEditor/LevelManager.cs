using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    [Header("JSON File Path")]
    public string jsonFileName = "level1.json";

    [Header("Cell References")]
    public Dictionary<string, GameObject> cellDictionary = new Dictionary<string, GameObject>();

    [Header("Environment Settings")]
    public GameObject[] environments; // Assign in Inspector
    public void LoadLevelData()
    {
        string path = Path.Combine(Application.dataPath, "../CustomMaps", jsonFileName);

        if (!File.Exists(path))
        {
            Debug.LogError("JSON file not found: " + path);
            return;
        }

        string json = File.ReadAllText(path);
        LevelData levelData = JsonUtility.FromJson<LevelData>(json);

        // 1. Deactivate all tiles
        foreach (var obj in cellDictionary.Values)
            obj.SetActive(false);

        // 2. Activate tiles from JSON
        foreach (string gCell in levelData.activeCells.G)
        {
            if (cellDictionary.TryGetValue(gCell, out GameObject obj))
                obj.SetActive(true);
        }

        foreach (string rCell in levelData.activeCells.R)
        {
            if (cellDictionary.TryGetValue(rCell, out GameObject obj))
                obj.SetActive(true);
        }

        // 3. Activate correct environment
        SetEnvironmentByName(levelData.activeEnvironmentName);

        Debug.Log($"Level loaded from: {path}");
    }

    private void SetEnvironmentByName(string name)
    {
        foreach (GameObject env in environments)
        {
            if (env == null) continue;
            env.SetActive(env.name == name);
        }

        Debug.Log($"Environment set to: {name}");
    }
    public void ScanField()
    {
        GameObject[] allTiles = Resources.FindObjectsOfTypeAll<GameObject>(); // includes inactive

        foreach (GameObject cell in allTiles)
        {
            if ((cell.name.StartsWith("G:") || cell.name.StartsWith("R:")) && cell.hideFlags == HideFlags.None)
            {
                cellDictionary[cell.name] = cell;
                cell.SetActive(true); // deactivate them manually
            }
        }
    }
    public void LoadLevel()
    {
        foreach (GameObject cell in FindObjectsOfType<GameObject>())
        {
            if (cell.name.StartsWith("G:") || cell.name.StartsWith("R:"))
            {
                cellDictionary[cell.name] = cell;
                cell.SetActive(true); // Ensure tiles are enabled before we deactivate later
            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F9))
        {
            ScanField();
            LoadLevel();       // FIRST: fill the dictionary
            LoadLevelData();   // THEN: load data from JSON
        }
    }
}
