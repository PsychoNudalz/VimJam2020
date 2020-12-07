using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// handles all the spawnZones
/// </summary>
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
    /// <summary>
    /// Get all Spawn Zones from are Children of this GameObject
    /// </summary>
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
    /// <summary>
    /// picks a random 2D Co-ordinate from a SpawnZone
    /// </summary>
    /// <returns> random 2D Co-ordinate </returns>
    public virtual Vector2 getRandomPos()
    {
        zonePTR = zonePTR % spawnZones.Count;
        Vector2 pos = spawnZones[zonePTR].getRandomPos();
        zonePTR++;
        return pos;
    }
    /// <summary>
    /// picks a random 2D Co-ordinate from a certain SpawnZone
    /// </summary>
    /// <param name="ptr">Index of spawnZone</param>
    /// <returns> random 2D Co-ordinate </returns>
    public virtual Vector2 getRandomPos(int ptr)
    {
        zonePTR = ptr;
        return getRandomPos();
    }

    //Get Selected Zones
    /// <summary>
    /// get a certain SpawnZone
    /// </summary>
    /// <param name="ptr">Index of the certain SpawnZone</param>
    /// <returns> a certain SpawnZone </returns>
    public virtual SpawnZoneScript getZone(int ptr)
    {
        zonePTR = ptr % spawnZones.Count;
        return spawnZones[zonePTR];
    }



}
