using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BarrierSpawnGroup;


public enum StageEventType
{
    SpawnEnemy,
    SpawnBoss,
}


[Serializable]
public class StageEvent
{
    public StageEventType eventType;

    public string message;

    public EmemyType enemyToSpawn;


    public int count;


}

[CreateAssetMenu]
public class StageData : ScriptableObject
{
    public int waveNum;
    public List<StageEvent> stageWaves;
}
