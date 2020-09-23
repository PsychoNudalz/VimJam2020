using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public virtual void resetSpawnArea_pick()
    {
        foreach (SpawnAreaScript s in spawnAreas)
        {
            spawnAreas_Pick.Add(s);
        }
    }
}
