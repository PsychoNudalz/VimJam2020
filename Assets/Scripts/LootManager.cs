using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour
{
    [Header("Generated Loot")]
    public List<GameObject> potentialLoot;
    public int totalLootCost = 6;
    [SerializeField] int totalLootCost_current;
    public List<SpawnRequest> lootSpawnRequestList;
    public List<GameObject> generatedLoot;

    [Header("Loot Spawn")]
    public SpawnZonesHandler_Loot spawnZonesHandler_Loot;

    private void Awake()
    {
        spawnZonesHandler_Loot = FindObjectOfType<SpawnZonesHandler_Loot>();
    }

    public void generateLoot(int cost)
    {
        totalLootCost = cost;
        generateLoot();
    }

    public void generateLoot()
    {
        totalLootCost_current = totalLootCost;
        SpawnRequest randomLoot;
        while (totalLootCost_current > 0)
        {
            randomLoot = new SpawnRequest(potentialLoot[Mathf.FloorToInt(Random.Range(0, potentialLoot.Count - 0.001f))],1);
            if (lootSpawnRequestList.Contains(randomLoot))
            {
                lootSpawnRequestList[lootSpawnRequestList.IndexOf(randomLoot)].modifyAmount(randomLoot.amount);
            }
            else
            {
                //print(randomLoot+ " not in list");
                lootSpawnRequestList.Add(randomLoot);
            }
            totalLootCost_current -= randomLoot.amount;
        }
    }

    public void spawnLoot()
    {
        foreach(SpawnRequest sq in lootSpawnRequestList)
        {
            for(int i = 0; i < sq.amount; i++)
            {
                try
                {
                generatedLoot.Add(Instantiate(sq.spawnObject, spawnZonesHandler_Loot.getRandomPos(), Quaternion.identity, transform));

                } catch(System.NullReferenceException _)
                {
                    spawnZonesHandler_Loot.getAllSpawnZones();
                    generatedLoot.Add(Instantiate(sq.spawnObject, spawnZonesHandler_Loot.getRandomPos(), Quaternion.identity, transform));

                }
            }
        }
    }
}
