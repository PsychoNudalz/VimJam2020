using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// handles the current wave/ group of enemies
/// </summary>
public class EnemyWaveHandler : MonoBehaviour
{
    public int waveNumber;
    public EnemyWaveTriggerType enemyWaveTriggerType = EnemyWaveTriggerType.ZONE;
    public List<GameObject> enemyList;
    public List<SpawnRequest> enemySpawnRequestList;
    public EnemySpawnZone enemySpawnZone;
    public bool spawnWave = false;

    public EnemyWaveHandler(int waveNumber, EnemyWaveTriggerType enemyWaveTriggerType)
    {
        this.waveNumber = waveNumber;
        this.enemyWaveTriggerType = enemyWaveTriggerType;
    }

    public void setEnemyWaveHandler(int waveNumber, EnemyWaveTriggerType enemyWaveTriggerType)
    {
        this.waveNumber = waveNumber;
        this.enemyWaveTriggerType = enemyWaveTriggerType;
    }


    /// <summary>
    /// loops through the spawn request list and spawn all required enemies at a random position and adds them to the list of current
    /// enemies in the level
    /// 
    /// enemies are also added to the turn order
    /// </summary>
    public void spawnEnemies()
    {
        enemySpawnZone = GetComponent<EnemySpawnZone>();
        enemyList = new List<GameObject>();
        foreach (SpawnRequest sq in enemySpawnRequestList)
        {
            for (int i = 0; i < sq.amount; i++)
            {
                try
                {
                    enemyList.Add(Instantiate(sq.spawnObject, enemySpawnZone.getRandomPos(), Quaternion.identity, transform));

                }
                catch (System.NullReferenceException _)
                {
                    enemySpawnZone = GetComponent<EnemySpawnZone>();
                    enemyList.Add(Instantiate(sq.spawnObject, enemySpawnZone.getRandomPos(), Quaternion.identity, transform));

                }
            }
        }

        addEnemiesToTurnOrder();
    }

    /// <summary>
    /// add the enemies from the enemyList to the turn order
    /// </summary>
    public void addEnemiesToTurnOrder()
    {
        BattleSystem b = FindObjectOfType<BattleSystem>();
        foreach (GameObject u in enemyList)
        {
            b.addTurn(u.GetComponent<UnitScript>());
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (enemyWaveTriggerType == EnemyWaveTriggerType.ZONE && !spawnWave)
        {
            if (collision.CompareTag("Player"))
            {

                spawnEnemies();
                spawnWave = true;
            }
        }
    }
}
