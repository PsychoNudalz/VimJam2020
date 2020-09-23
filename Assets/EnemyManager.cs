using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Generation Parameters")]
    public List<GameObject> potentialEnemies;
    public int totalEnemyCost = 10;
    public int waveAmount = 3;
    public int maxCost = 3;
    [SerializeField] int cost_Current;

    [Header("Enemy Spawn")]
    public SpawnZonesHandler_Enemy spawnZonesHandler_Enemy;

    //Hand Set
    public List<EnemyWaveHandler> enemyWaveHandlers;
    public GameObject wavePF;

    private void Awake()
    {
        spawnZonesHandler_Enemy = FindObjectOfType<SpawnZonesHandler_Enemy>();
    }

    public void generateWaves()
    {
        if (enemyWaveHandlers.Count == 0)
        {
            EnemyWaveHandler e;
            for (int i = 0; i < waveAmount; i++)
            {
                e = spawnZonesHandler_Enemy.getZone(i).gameObject.AddComponent<EnemyWaveHandler>();
                e.setEnemyWaveHandler(i, EnemyWaveTriggerType.ZONE);
                enemyWaveHandlers.Add(e);
            }
        }

    }
    public void setEnemyManager(int totalEnemyCost, int waveAmount, int maxCost)
    {
        this.totalEnemyCost = totalEnemyCost;
        this.waveAmount = waveAmount;
        this.maxCost = maxCost;
        generateEnemy();
    }


    public void generateEnemy()
    {
        generateWaves();
        List<SpawnRequest> enemySpawnRequestList;
        EnemyWaveHandler enemyWaveHandler;
        for (int i = 0; i<enemyWaveHandlers.Count;i++)
        {
            enemyWaveHandler = enemyWaveHandlers[i];
            cost_Current = Mathf.RoundToInt(totalEnemyCost / waveAmount);
            enemySpawnRequestList = new List<SpawnRequest>();
            SpawnRequest randomEnemySpawnRequest;
            GameObject randomEnemy = pickRandomEnemy();
            while (cost_Current > 0)
            {
                randomEnemy = pickRandomEnemy();

                while (randomEnemy.GetComponent<AIUnitScript>().level > maxCost)
                {
                    randomEnemy = randomEnemy = pickRandomEnemy();
                }

                randomEnemySpawnRequest = new SpawnRequest(randomEnemy, 1);
                if (enemySpawnRequestList.Contains(randomEnemySpawnRequest))
                {
                    enemySpawnRequestList[enemySpawnRequestList.IndexOf(randomEnemySpawnRequest)].modifyAmount(randomEnemySpawnRequest.amount);
                }
                else
                {
                    //print(randomEnemy+ " not in list");
                    enemySpawnRequestList.Add(randomEnemySpawnRequest);
                }
                if (randomEnemySpawnRequest.spawnObject.TryGetComponent(out AIUnitScript a))
                {
                    cost_Current -= a.level;
                }
                else
                {
                    cost_Current -= 1;
                }
            }
            enemyWaveHandler.enemySpawnRequestList = enemySpawnRequestList;
        }
    }

    public GameObject pickRandomEnemy()
    {
        GameObject enemy;
        try
        {
            enemy = potentialEnemies[Mathf.FloorToInt(Random.Range(0, maxCost))];
            return enemy;
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("Tried to spawn enemy from list out of range");
            return pickRandomEnemy();
        }
    }

    public void spawnEnemies()
    {
        foreach(EnemyWaveHandler enemyWaveHandler in enemyWaveHandlers)
        {
            enemyWaveHandler.spawnEnemies();
        }
    }
}
