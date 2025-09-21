using FischlWorks_FogWar;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Monster_Generate : MonoBehaviour
{
    private float timerGen;
    private float timerStageDelay;
    public float stageDelay;
    private bool bossLVL;

    public int monsters;
    public int aliveMonsters;
    public GameObject[] game_units;
    public GameObject[] game_bosses;
    private int boos_number;

    public Transform target;
    public Transform target_2;
    public Transform target_3;
    public bool createWave;
    public bool mainMenu;

    public GameObject waveDelayTimer;
    public GameObject stageCounter;
    public GameObject roadNavigationSystem;
    public GameObject fogOfWar;

    private LevelData levelData;

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

    void Start()
    {
        roadNavigationSystem.GetComponent<NavMeshManager>().UpdateNavMesh();
        timerGen = 0f;
        if (!mainMenu) stage = 0;
        monsters = 0;
        aliveMonsters = 0;
        timerStageDelay = stageDelay;
        bossLVL = false;
        createWave = false;

        LoadLevelData();
    }

    void Update()
    {
        timerGen += Time.deltaTime;
        if (!fogOfWarActivated) fogOfWar.GetComponent<csFogWar>().DeactivateFogOfWar();

        if (monsters <= population && aliveMonsters <= 0 && stage <= 49) monsters = 0;

        if ((monsters == 0 && stage <= 49 && aliveMonsters <= 0) || mainMenu)
            DelayBeforeWave();

        StartCoroutine(OnGeneratingRoutine());
    }

    void LoadLevelData()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "level1.json");

        if (!File.Exists(path))
        {
            Debug.LogError("Level JSON not found: " + path);
            return;
        }

        string json = File.ReadAllText(path);
        levelData = JsonUtility.FromJson<LevelData>(json);

        Debug.Log("Loaded level data for: " + levelData.levelName);
    }

    void ApplyStageStats()
    {
        int currentStage = Mathf.FloorToInt(stage);
        StageData stageData = levelData.stages.Find(s => s.stage == currentStage);
        if (stageData == null)
        {
            Debug.LogError("No data for stage " + currentStage);
            return;
        }

        enemyModelIndex = stageData.enemyModelIndex;
        population = stageData.population;
        delayGeneration = stageData.delayGeneration;

        e_Speed = stageData.e_Speed;
        e_maxHP = stageData.e_maxHP;
        damage = stageData.damage;
        attackSpeed = stageData.attackSpeed;
        attackRange = stageData.attackRange;
        freezePower = stageData.freezePower;
        interestZone = stageData.interestZone;
        coinReward = stageData.coinReward;
        expReward = stageData.expReward;

        goToFinish = stageData.goToFinish;
        goToPlayer = stageData.goToPlayer;
        goToTower = stageData.goToTower;
        goToNpc = stageData.goToNpc;
        fogOfWarActivated = stageData.fogOfWarActivated;

        aliveMonsters = population;

        if (goToPlayer || goToNpc || goToTower)
            roadNavigationSystem.GetComponent<NavMeshManager>().MakeAllWalkable();
        else
            roadNavigationSystem.GetComponent<NavMeshManager>().MakeOnlyRoadWalkable();
    }

    public void DelayBeforeWave()
    {
        if (timerStageDelay > 0)
        {
            createWave = false;
            fogOfWarActivated = false;

            if (waveDelayTimer != null) waveDelayTimer.GetComponent<UICounter>().ActivateUI();

            timerStageDelay -= Time.deltaTime;
            int displayTimeInt = (int)timerStageDelay;
            if (waveDelayTimer != null) waveDelayTimer.GetComponent<UICounter>().TakeCounterData(displayTimeInt);
        }

        if (timerStageDelay <= 0)
        {
            StartWave();
            CreateBoss();
        }
    }

    public void StartWave()
    {
        if (waveDelayTimer != null) waveDelayTimer.GetComponent<UICounter>().DeactivateUI();
        createWave = true;
        stage += 1;
        ApplyStageStats();

        if (fogOfWarActivated)
            fogOfWar.GetComponent<csFogWar>().ActivateFogOfWar();

        if (stageCounter != null)
            stageCounter.GetComponent<UICounter>().TakeCounterData(stage);
    }

    public void KillMonster()
    {
        aliveMonsters -= 1;
    }

    public void CreateBoss()
    {
        if (!bossLVL) return;

        Vector3 position = target.position;
        int randomPosition = 0;

        if (target_2 != null) randomPosition = Random.Range(1, 3);
        if (target_3 != null) randomPosition = Random.Range(1, 4);
        if (randomPosition == 2) position = target_2.position;
        if (randomPosition > 2) position = target_3.position;

        GameObject boss = Instantiate(game_bosses[enemyModelIndex], position, Quaternion.identity);
        boss.name = "Boss_" + stage + "_" + stage;
        Enemy_AI ai = boss.GetComponent<Enemy_AI>();
        Enemy_stats stats = boss.GetComponent<Enemy_stats>();
        stats.GetSpeedStatStat(e_Speed);
        stats.GetMaxHPStatStat(e_maxHP);
        stats.GetCoinRewardStat(coinReward);
        stats.GetExpRewardStat(expReward);

        ai.GetDamageStat(damage);
        ai.GetAttackSpeedStat(attackSpeed);
        ai.GetAttackRangeStat(attackRange);
        ai.GetFreezePowerStat(freezePower);
        ai.GetInterestZoneStat(interestZone);

        ai.GoToFinish(goToFinish);
        ai.GoToPlayer(goToPlayer);
        ai.GoToTower(goToTower);
        ai.GoToNpc(goToNpc);
        boss.SetActive(true);

        bossLVL = false;
    }

    public IEnumerator OnGeneratingRoutine()
    {
        if (!createWave || monsters >= population || timerGen <= delayGeneration)
            yield break;

        Vector3 position = target.position;
        int randomPosition = 0;
        if (target_2 != null) randomPosition = Random.Range(1, 3);
        if (target_3 != null) randomPosition = Random.Range(1, 4);
        if (randomPosition == 2) position = target_2.position;
        if (randomPosition > 2) position = target_3.position;

        //GameObject enemy = Instantiate(game_units[Random.Range(firstEnemyNumber, lastEnemyNumber)], position, Quaternion.identity);
        GameObject enemy = Instantiate(game_units[enemyModelIndex], position, Quaternion.identity);
        enemy.name = "Monster_" + monsters + "_" + stage;

        Enemy_AI ai = enemy.GetComponent<Enemy_AI>();
        Enemy_stats stats = enemy.GetComponent<Enemy_stats>();
        stats.GetSpeedStatStat(e_Speed);
        stats.GetMaxHPStatStat(e_maxHP);
        stats.GetCoinRewardStat(coinReward);
        stats.GetExpRewardStat(expReward);

        ai.GetDamageStat(damage);
        ai.GetAttackSpeedStat(attackSpeed);
        ai.GetAttackRangeStat(attackRange);
        ai.GetFreezePowerStat(freezePower);
        ai.GetInterestZoneStat(interestZone);

        ai.GoToFinish(goToFinish);
        ai.GoToPlayer(goToPlayer);
        ai.GoToTower(goToTower);
        ai.GoToNpc(goToNpc);
        enemy.SetActive(true);

        monsters += 1;
        timerGen = 0f;
        timerStageDelay = stageDelay;

        yield return new WaitForEndOfFrame();
    }
}
