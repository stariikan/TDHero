using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public string levelName;
    public string activeEnvironmentName; // NEW: Track active environment name
    public ActiveCells activeCells;
    public List<StageData> stages = new List<StageData>();
}
[System.Serializable]
public class StageData
{
    public int enemyModelIndex;
    public int stage;
    public int population;
    public float delayGeneration;
    public float e_Speed;
    public float e_maxHP;
    public float damage;
    public float attackSpeed;
    public float attackRange;
    public float freezePower;
    public float interestZone;
    public float coinReward;
    public float expReward;
    public bool goToFinish;
    public bool goToPlayer;
    public bool goToTower;
    public bool goToNpc;
    public bool fogOfWarActivated;
}
[System.Serializable]
public class ActiveCells
{
    public List<string> G = new List<string>(); // Green cells
    public List<string> R = new List<string>(); // Road cells
}
public class LevelSaver : MonoBehaviour
{
    [Header("Export Settings")]
    public string levelName = "Level 1";
    public string fileName = "level1.json";
    public GameObject[] environments; // NEW: Assign this in Inspector

    [ContextMenu("Export Active Tiles to JSON")]
    public void ExportActiveTiles()
    {
        LevelData levelData = new LevelData
        {
            levelName = this.levelName,
            activeCells = new ActiveCells(),
            activeEnvironmentName = GetActiveEnvironmentName() // Add active environment
        };

        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (!obj.activeInHierarchy) continue;

            string name = obj.name;

            if (name.StartsWith("G:"))
                levelData.activeCells.G.Add(name);
            else if (name.StartsWith("R:"))
                levelData.activeCells.R.Add(name);
        }

        string json = JsonUtility.ToJson(levelData, true);
        string folderPath = Path.Combine(Application.dataPath, "../CustomMaps");
        if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

        string fullPath = Path.Combine(folderPath, fileName);
        File.WriteAllText(fullPath, json);

        Debug.Log($"Level exported to: {fullPath}");
        Debug.Log($"Active Environment Saved: {levelData.activeEnvironmentName}");
    }

    private string GetActiveEnvironmentName()
    {
        foreach (GameObject env in environments)
        {
            if (env != null && env.activeSelf)
                return env.name;
        }
        return "Unknown";
    }
    public void ChangeMap()
    {
        int currentIndex = -1;

        // Find the currently active environment
        for (int i = 0; i < environments.Length; i++)
        {
            if (environments[i] != null && environments[i].activeSelf)
            {
                currentIndex = i;
                environments[i].SetActive(false);
                break;
            }
        }

        // Determine the next index (loop back to 0 if at the end)
        int nextIndex = (currentIndex + 1) % environments.Length;

        // Activate the next environment
        if (environments[nextIndex] != null)
        {
            environments[nextIndex].SetActive(true);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            ExportActiveTiles();
        }
    }
}

