using TMPro;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using UnityEngine.UI;

public class StageSaver : MonoBehaviour
{
    public TMP_Dropdown enemyModelDropdown;
    public TMP_InputField stageInput;
    public TMP_InputField populationInput;
    public TMP_InputField delayGenInput;
    public TMP_InputField speedInput;
    public TMP_InputField hpInput;
    public TMP_InputField damageInput;
    public TMP_InputField atkSpeedInput;
    public TMP_InputField atkRangeInput;
    public TMP_InputField freezePowerInput;
    public TMP_InputField interestZoneInput;
    public TMP_InputField coinRewardInput;
    public TMP_InputField expRewardInput;

    public Toggle finishToggle;
    public Toggle playerToggle;
    public Toggle towerToggle;
    public Toggle npcToggle;
    public Toggle fogToggle;

    public string fileName = "level1.json";

    public void SaveStage()
    {
        string path = Path.Combine(Application.streamingAssetsPath, fileName);
        LevelData data;

        // Load or create
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            data = JsonUtility.FromJson<LevelData>(json);
        }
        else
        {
            data = new LevelData { levelName = "Level 1", activeCells = new ActiveCells(), stages = new List<StageData>() };
        }

        // Create new stage
        StageData newStage = new StageData
        {
            enemyModelIndex = enemyModelDropdown.value,
            stage = int.Parse(stageInput.text),
            population = int.Parse(populationInput.text),
            delayGeneration = float.Parse(delayGenInput.text),
            e_Speed = float.Parse(speedInput.text),
            e_maxHP = float.Parse(hpInput.text),
            damage = float.Parse(damageInput.text),
            attackSpeed = float.Parse(atkSpeedInput.text),
            attackRange = float.Parse(atkRangeInput.text),
            freezePower = float.Parse(freezePowerInput.text),
            interestZone = float.Parse(interestZoneInput.text),
            coinReward = float.Parse(coinRewardInput.text),
            expReward = float.Parse(expRewardInput.text),
            goToFinish = finishToggle.isOn,
            goToPlayer = playerToggle.isOn,
            goToTower = towerToggle.isOn,
            goToNpc = npcToggle.isOn,
            fogOfWarActivated = fogToggle.isOn
        };

        // Replace existing or add new
        int existingIndex = data.stages.FindIndex(s => s.stage == newStage.stage);
        if (existingIndex >= 0)
            data.stages[existingIndex] = newStage;
        else
            data.stages.Add(newStage);

        // Save
        string newJson = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, newJson);
        Debug.Log($"Stage {newStage.stage} saved to {path}");
    }
}