using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawnAreaScript : SpawnAreaScript
{
    public bool isSpawnOnPoint;


    public override Vector2 getRandomPos()
    {
        if (isSpawnOnPoint)
        {
            return (Vector2)transform.position;
        }
        return base.getRandomPos();
    }
}
