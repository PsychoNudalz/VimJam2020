using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [Header("Enemy Spawns")]
    public EnemyManager enemyManager;
    public int totalEnemyCost = 10;
    public int waveAmount = 3;
    public int maxCost = 3;

    [Header("Loot Spawns")]
    public LootManager lootManager;


    [Header("Objective")]
    public PlayerManagerScript player;
    public ObjectiveEnum objectiveEnum = ObjectiveEnum.COLLECTION;
    public int targetValue = 3;



    private void Awake()
    {
        if (lootManager == null)
        {
            lootManager = FindObjectOfType<LootManager>();
        }
        if (enemyManager == null)
        {
            enemyManager = FindObjectOfType<EnemyManager>();
        }

    }

    private void Start()
    {

    }

    private void FixedUpdate()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerManagerScript>();
            StartLevel(player.questType);

        }
    }

    public void StartLevel(QuestType q = null)
    {
        if (q != null)
        {
            objectiveEnum = q.objectiveEnum;
            targetValue = q.targetValue;
            totalEnemyCost = q.totalEnemyCost;
            waveAmount = q.waveAmount;
            maxCost = q.maxCost;
        }

        if (lootManager == null)
        {
            lootManager = FindObjectOfType<LootManager>();
        }
        lootManager.generateLoot(Mathf.RoundToInt((targetValue+1) * 1.5f));
        lootManager.spawnLoot();

        if (enemyManager == null)
        {
            enemyManager = FindObjectOfType<EnemyManager>();
        }
        enemyManager.setEnemyManager(totalEnemyCost, waveAmount, maxCost);
    }

    public bool checkPlayerComplete()
    {
        switch (objectiveEnum)
        {
            case (ObjectiveEnum.COLLECTION):
                return player.loot.Count >= targetValue;
        }

        return false;
    }

    public (string, Color) getMissionObjective()
    {

        if (player == null)
        {
            return (null, Color.white);
        }
        string tempS = "Objective:\nCollect pieces of loot: " + player.loot.Count + "/" + targetValue;
        if (player.loot.Count >= targetValue)
        {
            return (tempS, Color.green);

        }
        else if (player.loot.Count == 0)
        {
            return (tempS, Color.red);
        }
        else
        {
            return (tempS, Color.yellow);

        }
    }
}
