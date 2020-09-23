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

    public virtual Vector2 getRandomPos()
    {
        zonePTR = zonePTR % spawnZones.Count;
        Vector2 pos = spawnZones[zonePTR].getRandomPos();
        zonePTR++;
        return pos;
    }


}
