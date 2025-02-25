using System.Collections;
using UnityEngine;

public class Monster_Generate : MonoBehaviour
{
    private float timerGen;                 // Timer for enemy geration
    public float delayGeneration;          // Delay between enemy generation
    private float timerStageDelay;        // Timer for stage delay  
    public float stageDelay;             // Time between stages
    private bool bossLVL;

    public int population;             // How much enemies will be created
    public int monsters;              // Enemies that was created
    public int aliveMonsters;       // How much monster still alive
    public GameObject[] game_units; // List of the enemies that will be created
    public GameObject[] game_bosses; // List of the enemies that will be created
    private int boos_number;
    public Transform target;       // Position where wave will be created
    public bool createWave;       // Bool for active wave
    public float stage;          // Stage number

    public GameObject waveDelayTimer; // UI Wave timer
    public GameObject stageCounter;  // UI Stage counter

    private int firstEnemyNumber; // Enemy number to chose wich enemy will be on the stage
    private int lastEnemyNumber; // Enemy number to chose wich enemy will be on the stage

    void Start()
    {
        if (population < 0) population = 0;
        createWave = false;
        timerGen = 0f;
        stage = 0;
        monsters = 0;
        aliveMonsters = 0;
        timerStageDelay = stageDelay;
        bossLVL = false;
    }
    // Update is called once per frame
    void Update()
    {
        timerGen += Time.deltaTime;
        if (monsters <= population && aliveMonsters <= 0 && stage <= 29) monsters = 0;
        if (monsters == 0 && stage <= 29 && aliveMonsters <= 0) DelayBeforeWave();
        StartCoroutine(OnGeneratingRoutine());
    }
    public void DelayBeforeWave()
    {
        if (timerStageDelay > 0)
        {
            createWave = false;
            waveDelayTimer.GetComponent<UICounter>().ActivateUI();
            timerStageDelay -= Time.deltaTime;
            int displayTimeInt = (int)timerStageDelay;
            float displayTimeFloat = (float)displayTimeInt;
            waveDelayTimer.GetComponent<UICounter>().TakeCounterData(displayTimeFloat);
        }
        if (timerStageDelay <= 0)
        {
            StartWave();
            CreateBoss();
        }
    }
    public void StartWave()
    {
        waveDelayTimer.GetComponent<UICounter>().DeactivateUI();
        createWave = true;
        stage += 1;
        StagesStats();
        stageCounter.GetComponent<UICounter>().TakeCounterData(stage);
        waveDelayTimer.GetComponent<UICounter>().TakeCounterData(timerStageDelay);
    }
    public void KillMonster()
    {
        aliveMonsters -= 1;
    }
    public void StagesStats()
    {
        if (stage == 1)
        {
            population = 12;
            delayGeneration = 2;
            firstEnemyNumber = 0;
            lastEnemyNumber = 0;
        }
        if (stage == 2)
        {
            population = 14;
            delayGeneration = 2;
            firstEnemyNumber = 1;
            lastEnemyNumber = 1;
        }
        if (stage == 3)
        {
            population = 16;
            delayGeneration = 2;
            firstEnemyNumber = 2;
            lastEnemyNumber = 2;
        }
        if (stage == 4)
        {
            population = 20;
            delayGeneration = 1;
            firstEnemyNumber = 3;
            lastEnemyNumber = 3;
        }
        if (stage == 5)
        {
            population = 20;
            delayGeneration = 1;
            firstEnemyNumber = 4;
            lastEnemyNumber = 4;
        }
        if (stage == 6)
        {
            population = 20;
            delayGeneration = 1;
            firstEnemyNumber = 5;
            lastEnemyNumber = 5;
        }
        if (stage == 7)
        {
            population = 20;
            delayGeneration = 1;
            firstEnemyNumber = 6;
            lastEnemyNumber = 6;
        }
        if (stage == 8)
        {
            population = 20;
            delayGeneration = 1;
            firstEnemyNumber = 7;
            lastEnemyNumber = 7;
        }
        if (stage == 9)
        {
            population = 20;
            delayGeneration = 1;
            firstEnemyNumber = 4;
            lastEnemyNumber = 7;
        }
        if (stage == 10)
        {
            population = 12;
            delayGeneration = 1;
            firstEnemyNumber = 8;
            lastEnemyNumber = 8;
            boos_number = 0;
            bossLVL = true;
        }
        if (stage == 11)
        {
            population = 20;
            delayGeneration = 1;
            firstEnemyNumber = 9;
            lastEnemyNumber = 9;
        }
        if (stage == 12)
        {
            population = 22;
            delayGeneration = 1;
            firstEnemyNumber = 10;
            lastEnemyNumber = 10;
        }
        if (stage == 13)
        {
            population = 22;
            delayGeneration = 1;
            firstEnemyNumber = 11;
            lastEnemyNumber = 11;
        }
        if (stage == 14)
        {
            population = 24;
            delayGeneration = 1;
            firstEnemyNumber = 5;
            lastEnemyNumber = 11;
        }
        if (stage == 15)
        {
            population = 24;
            delayGeneration = 1;
            firstEnemyNumber = 12;
            lastEnemyNumber = 12;
        }
        if (stage == 16)
        {
            population = 24;
            delayGeneration = 1;
            firstEnemyNumber = 13;
            lastEnemyNumber = 13;
        }
        if (stage == 17)
        {
            population = 24;
            delayGeneration = 1;
            firstEnemyNumber = 14;
            lastEnemyNumber = 14;
        }
        if (stage == 18)
        {
            population = 26;
            delayGeneration = 1;
            firstEnemyNumber = 12;
            lastEnemyNumber = 14;
        }
        if (stage == 19)
        {
            population = 26;
            delayGeneration = 1;
            firstEnemyNumber = 15;
            lastEnemyNumber = 15;
        }
        if (stage == 20)
        {
            population = 15;
            delayGeneration = 1;
            firstEnemyNumber = 16;
            lastEnemyNumber = 16;
            boos_number = 1;
            bossLVL = true;
        }
        if (stage == 21)
        {
            population = 26;
            delayGeneration = 1;
            firstEnemyNumber = 17;
            lastEnemyNumber = 17;
        }
        if (stage == 22)
        {
            population = 26;
            delayGeneration = 1;
            firstEnemyNumber = 18;
            lastEnemyNumber = 18;
        }
        if (stage == 23)
        {
            population = 28;
            delayGeneration = 1;
            firstEnemyNumber = 15;
            lastEnemyNumber = 18;
        }
        if (stage == 24)
        {
            population = 28;
            delayGeneration = 1;
            firstEnemyNumber = 19;
            lastEnemyNumber = 19;
        }
        if (stage == 25)
        {
            population = 28;
            delayGeneration = 1;
            firstEnemyNumber = 20;
            lastEnemyNumber = 20;
        }
        if (stage == 26)
        {
            population = 28;
            delayGeneration = 1;
            firstEnemyNumber = 21;
            lastEnemyNumber = 21;
        }
        if (stage == 27)
        {
            population = 30;
            delayGeneration = 1;
            firstEnemyNumber = 18;
            lastEnemyNumber = 21;
        }
        if (stage == 28)
        {
            population = 30;
            delayGeneration = 1;
            firstEnemyNumber = 22;
            lastEnemyNumber = 22;
        }
        if (stage == 29)
        {
            population = 30;
            delayGeneration = 1;
            firstEnemyNumber = 23;
            lastEnemyNumber = 23;
        }
        if (stage == 30)
        {
            population = 15;
            delayGeneration = 1;
            firstEnemyNumber = 24;
            lastEnemyNumber = 24;
            boos_number = 2;
            bossLVL = true;
        }
        aliveMonsters = population;
    }
    public void CreateBoss()
    {
        Vector3 position = target.position;
        if (bossLVL)
        {
            GameObject boss = Instantiate(game_bosses[boos_number], new Vector3(position.x, position.y, position.z), Quaternion.identity);
            boss.name = "Boss_" + stage + "_" + stage;
            boss.gameObject.SetActive(true);
            bossLVL = false;
        }
    }
    public IEnumerator OnGeneratingRoutine()
    {
        Vector3 position = target.position;
        if (monsters < population && timerGen > delayGeneration && createWave) 
        {
            GameObject enemy = Instantiate(game_units[Random.Range(firstEnemyNumber, lastEnemyNumber)], new Vector3(position.x, position.y, position.z), Quaternion.identity);
            enemy.name = "Monster_" + monsters + "_" + stage;
            enemy.gameObject.SetActive(true);
            monsters += 1;
            timerGen = 0f;
            timerStageDelay = stageDelay;
        }
        yield return new WaitForEndOfFrame(); // waiting for blocks to be installed

    }
}