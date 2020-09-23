using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnZone : SpawnZoneScript
{
    public override List<SpawnAreaScript> findAllAreasInZone()
    {
        spawnAreas = new List<SpawnAreaScript>();
        foreach(EnemySpawnAreaScript g in transform.GetComponentsInChildren<EnemySpawnAreaScript>())
        {
            spawnAreas.Add(g);
        }
        return spawnAreas;
    }
}
