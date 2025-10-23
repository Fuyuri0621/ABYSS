using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;
using static BarrierSpawnGroup;




public class EnemyManger : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject boss;
    [SerializeField] List<Transform> spawnPoints;
    [SerializeField] float spawntime;

    public int enemycount;

   



    List<BarrierSpawnGroup> enemiesSpawnGroupsList;
    List<BarrierSpawnGroup> repeatedSpawnGroupList;


    private void Start()
    {



    }

    private void Update()
    {
        ProcessSpawn();
        ProcessRepeatedSpawnGroups();
    }

    private void ProcessRepeatedSpawnGroups()
    {
        if (repeatedSpawnGroupList == null) { return; }
        for (int i = repeatedSpawnGroupList.Count - 1; i >= 0; i--)
        {
            repeatedSpawnGroupList[i].repeatTimer -= Time.deltaTime;
            if (repeatedSpawnGroupList[i].repeatTimer < 0)
            {
                repeatedSpawnGroupList[i].repeatTimer = repeatedSpawnGroupList[i].timeBetweenSpawn;
                AddGroupToSpawn(repeatedSpawnGroupList[i].barrierType, repeatedSpawnGroupList[i].count);
                repeatedSpawnGroupList[i].repeatCount -= 1;
                if (repeatedSpawnGroupList[i].repeatCount <= 0)
                {
                    repeatedSpawnGroupList.RemoveAt(i);
                }
            }
        }
    }

    private void ProcessSpawn()
    {
        if (enemiesSpawnGroupsList == null) { return; }

        if (enemiesSpawnGroupsList.Count > 0)
        {
            SpawnEnemy(enemiesSpawnGroupsList[0].barrierType);
            enemiesSpawnGroupsList[0].count -= 1;
            if (enemiesSpawnGroupsList[0].count <= 0)
            {
                enemiesSpawnGroupsList.RemoveAt(0);
            }
        }
    }



    public void AddGroupToSpawn(EmemyType barrierTospawn, int count)
    {
        BarrierSpawnGroup newGroupToSpawn = new BarrierSpawnGroup(barrierTospawn, count);

        if (enemiesSpawnGroupsList == null) { enemiesSpawnGroupsList = new List<BarrierSpawnGroup>(); }

        enemiesSpawnGroupsList.Add(newGroupToSpawn);
    }

    public void SpawnEnemy(EmemyType barrierToSpawn)
    {
        Transform ememyspawntransform;
        int f = UnityEngine.Random.Range(0, spawnPoints.Count);

        ememyspawntransform = spawnPoints[f];
           






        GameObject newEnemy = Instantiate(enemy);
        newEnemy.transform.position = ememyspawntransform.position;
       newEnemy.GetComponent<BattleEnemy>().manger=this;



        newEnemy.transform.parent = transform;
        enemycount++;

    }
    public void SpawnBoss()
    {
        Transform ememyspawntransform;
        int f = UnityEngine.Random.Range(0, spawnPoints.Count);

        ememyspawntransform = spawnPoints[f];







        GameObject newEnemy = Instantiate(boss);
        newEnemy.transform.position = ememyspawntransform.position;
        newEnemy.GetComponent<BattleEnemy>().manger = this;



        newEnemy.transform.parent = transform;


    }





}

public class BarrierSpawnGroup
{
    public EmemyType barrierType;
    public int count;
    public float repeatTimer;
    public float timeBetweenSpawn;
    public int repeatCount;

    public BarrierSpawnGroup(EmemyType barrierType, int count)
    {
        this.barrierType = barrierType;
        this.count = count;

    }
    public void SetRepeatSpawn(float timeBetweenSpawns, int repeatCount)
    {
        this.timeBetweenSpawn = timeBetweenSpawns;
        this.repeatCount = repeatCount;
        repeatTimer = timeBetweenSpawn;
    }

    public enum EmemyType
    {
        Block,
        Damage,


    }
}
