using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZonesHandler : MonoBehaviour
{
    public List<SpawnZoneScript> spawnZones;
    int zonePTR = 0;

    /*
    public virtual List<SpawnZoneScript> findAllZones()
    {
        spawnZones = new List<SpawnZoneScript>();
        foreach (LootSpawnAreaScript g in transform.GetComponentsInChildren<LootSpawnAreaScript>())
        {
            spawnZones.Add(g);
        }
        return spawnZones;
    }
    */

    private void Start()
    {
        if (spawnZones.Count == 0)
        {
            getAllSpawnZones();
        }
    }

    public void getAllSpawnZones()
    {
        spawnZones = new List<SpawnZoneScript>();
        foreach (SpawnZoneScript g in transform.GetComponentsInChildren<SpawnZoneScript>())
        {
            spawnZones.Add(g);
        }
        return ;
    }


    //Get Random Spawn
    public virtual Vector2 getRandomPos()
    {
        zonePTR = zonePTR % spawnZones.Count;
        Vector2 pos = spawnZones[zonePTR].getRandomPos();
        zonePTR++;
        return pos;
    }

    public virtual Vector2 getRandomPos(int ptr)
    {
        zonePTR = ptr;
        return getRandomPos();
    }

    //Get Selected Zones
    public virtual SpawnZoneScript getZone(int ptr)
    {
        zonePTR = ptr % spawnZones.Count;
        return spawnZones[zonePTR];
    }



}
