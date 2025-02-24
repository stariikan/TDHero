using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TD_Timer : MonoBehaviour
{
    public float globaltime;
    public int timeSpeedMultiplicator;
    public int coins;
    public bool playerReady;
    public bool nextWave;
    public float timeBetweenWave;
    public static TD_Timer Instance { get; set; } // To collect and send data from this script
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        playerReady = false;
        nextWave = true;
        if (globaltime < 0) globaltime = 0;
        if (timeSpeedMultiplicator <= 0) timeSpeedMultiplicator = 1;

    }
    // Update is called once per frame
    void Update()
    {
        GlobalTimeScrole();
        NewWave();
        if (Input.GetKeyDown(KeyCode.E)) IncreaseTimeSpeed();
        if (Input.GetKeyDown(KeyCode.Q)) DecreaseTimeSpeed();
        if (Input.GetKeyDown(KeyCode.Mouse2)) StopTimeSpeed();
        if (Input.GetKeyDown(KeyCode.R)) NormalTimeSpeed();
    }
    public void IncreaseTimeSpeed()
    {
        if (timeSpeedMultiplicator < 100) timeSpeedMultiplicator += 1;
        Debug.Log("Increase Time Speed" + " = " + timeSpeedMultiplicator);
    }
    public void DecreaseTimeSpeed()
    {
        if (timeSpeedMultiplicator > 0) timeSpeedMultiplicator -= 1;
        Debug.Log("Decrease Time Speed" + " = " + timeSpeedMultiplicator);
    }
    public void StopTimeSpeed()
    {
        timeSpeedMultiplicator = 0;
        Debug.Log("Stop Time Speed" + " = " + timeSpeedMultiplicator);
    }
    public void NormalTimeSpeed()
    {
        timeSpeedMultiplicator = 1;
        Debug.Log("Normal Time Speed" + " = " + timeSpeedMultiplicator);
    }
    public void GlobalTimeScrole()
    {
        globaltime = globaltime + (Time.deltaTime * timeSpeedMultiplicator);
    }
    public void EarnCoins(int increaseCoin)
    {
        coins += increaseCoin;
    }
    public void SpendCoin(int reduceCoin)
    {
        coins -= reduceCoin;
    }
    public void WaveIsReady()
    {
        nextWave = false;
    }
    public void EndWave()
    {
        globaltime = 0;
        nextWave = true;
    }
    public void NewWave()
    {
        if (globaltime > timeBetweenWave && playerReady == false && nextWave == true)
        {
            Debug.Log("DAAAA");
            this.gameObject.GetComponent<Monster_Generate>().StartWave();
        }
        if (playerReady == true && nextWave == true)
        {
            this.gameObject.GetComponent<Monster_Generate>().StartWave();
        }
    }
}