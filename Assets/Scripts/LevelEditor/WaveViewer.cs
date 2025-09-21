using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveViewer : MonoBehaviour
{
    public string jsonFileName = "level1.json";
    public TextMeshProUGUI viewerText; // Assign from UI
    private string filePath;

    private void Start()
    {
        filePath = Path.Combine(Application.streamingAssetsPath, jsonFileName);
    }

    [ContextMenu("Load Wave Viewer")]
    public void LoadWaveData()
    {
        if (!File.Exists(filePath))
        {
            Debug.LogError("Level JSON not found at: " + filePath);
            return;
        }

        string json = File.ReadAllText(filePath);
        LevelData levelData = JsonUtility.FromJson<LevelData>(json);

        if (levelData.stages == null || levelData.stages.Count == 0)
        {
            viewerText.text = "No stages found.";
            return;
        }

        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        foreach (var stage in levelData.stages)
        {
            sb.AppendLine($"<b>Stage {stage.stage}</b>");
            sb.AppendLine($"Enemy Model #: {stage.enemyModelIndex}");
            sb.AppendLine($"Population: {stage.population}");
            sb.AppendLine($"Spawn Delay: {stage.delayGeneration}");
            sb.AppendLine($"Speed: {stage.e_Speed}");
            sb.AppendLine($"HP: {stage.e_maxHP}");
            sb.AppendLine($"Damage: {stage.damage}");
            sb.AppendLine($"Attack Speed: {stage.attackSpeed}");
            sb.AppendLine($"Attack Range: {stage.attackRange}");
            sb.AppendLine($"Freeze Power: {stage.freezePower}");
            sb.AppendLine($"Interest Zone: {stage.interestZone}");
            sb.AppendLine($"Coin Reward: {stage.coinReward}");
            sb.AppendLine($"EXP Reward: {stage.expReward}");
            sb.AppendLine($"Go To Finish: {stage.goToFinish}");
            sb.AppendLine($"Go To Player: {stage.goToPlayer}");
            sb.AppendLine($"Go To Tower: {stage.goToTower}");
            sb.AppendLine($"Go To NPC: {stage.goToNpc}");
            sb.AppendLine($"Fog of War: {stage.fogOfWarActivated}");
            sb.AppendLine("<size=10>───────────────────────────────</size>");
        }

        viewerText.text = sb.ToString();
    }
}