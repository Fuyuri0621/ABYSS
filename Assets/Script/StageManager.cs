using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class StageEventManager : MonoBehaviour
{
    [SerializeField] public StageData stageData;
    [SerializeField] EnemyManger enemiesManger;
    [SerializeField] GameObject player;



    int waveIndexer=0;



    private void Awake()
    {

    }

    private void Start()
    {
       
    }
    private void Update()
    {
        if (waveIndexer > stageData.stageWaves.Count) { return; }
        if (enemiesManger.enemycount <= 0 && waveIndexer == stageData.stageWaves.Count) { SpawnBoss(); waveIndexer++;return; }
        if (enemiesManger.enemycount <= 0)
        {
            
            SpawnEnemy();
            waveIndexer++;
        }
       
    }

  

    private void SpawnEnemy()
    {
        StageEvent currentWave = stageData.stageWaves[waveIndexer];
        enemiesManger.AddGroupToSpawn(currentWave.enemyToSpawn, currentWave.count);



    }
    private void SpawnBoss()
    {

        enemiesManger.SpawnBoss();



    }

}
