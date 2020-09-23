using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [Header("Enemy Spawns")]
    public List<GameObject> potentialEnemies;
    public int totalEnemyCost = 10;
    public int waveAmount = 3;
    public int maxCost = 3;
    public List<SpawnRequest> enemySpawnRequestList;

    [Header("Loot Spawns")]
    public LootManager lootManager;


    [Header("Objective")]
    public ObjectiveEnum objectiveEnum = ObjectiveEnum.COLLECTION;
    public int targetValue = 3;


    private void Awake()
    {
        if (lootManager == null)
        {
            lootManager = FindObjectOfType<LootManager>();
        }
    }

    private void Start()
    {
        StartLevel();
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
        lootManager.generateLoot(Mathf.RoundToInt(targetValue * 1.5f));
        lootManager.spawnLoot();
    }
}
