using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Pick a random 2D Co-ordante from a list of SpawnAreas
/// </summary>
public class SpawnZoneScript : MonoBehaviour
{
    [Header("Spawn Areas")]
    public List<SpawnAreaScript> spawnAreas;

    [Header("Iterator")]
    public int areaPointer = 0;
    public List<SpawnAreaScript> spawnAreas_Pick;

    private void Awake()
    {
        findAllAreasInZone();
    }


    /// <summary>
    /// Pick a random spawnArea to get a spawn point from
    /// spawnArea will be duplicated until all spawnArea has been pick
    /// </summary>
    /// <returns>random 2D Co-ordinate from a random spawnArea</returns>
    public virtual Vector2 getRandomPos()
    {
        if (spawnAreas_Pick.Count == 0)
        {
            resetSpawnArea_pick();
        }
        try
        {
            areaPointer = Random.Range(0, spawnAreas_Pick.Count);

        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e);
            return new Vector2();
        }
        Vector2 pos = spawnAreas_Pick[areaPointer].getRandomPos();
        spawnAreas_Pick.RemoveAt(areaPointer);

        return pos;

    }

    /// <summary>
    /// returns the list of spawnAreas
    /// </summary>
    /// <returns>spawnAreas</returns>
    public virtual List<SpawnAreaScript> findAllAreasInZone()
    {
        return spawnAreas;
    }

    /*
    public bool checkIfAreaInRange(Vector2 pos)
    {
        bool x = transform.position.x-transform.lossyScale/
    }
    */
    /// <summary>
    /// reset list spawnAreas_Pick for picking a random spawnArea
    /// </summary>
    public virtual void resetSpawnArea_pick()
    {

        spawnAreas_Pick = new List<SpawnAreaScript>(spawnAreas);
    }
}
