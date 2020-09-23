using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawnZone : SpawnZoneScript
{
    public override List<SpawnAreaScript> findAllAreasInZone()
    {
        spawnAreas = new List<SpawnAreaScript>();
        foreach(LootSpawnAreaScript g in transform.GetComponentsInChildren<LootSpawnAreaScript>())
        {
            spawnAreas.Add(g);
        }
        return spawnAreas;
    }
}
