using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// tuple storing what needs game object needs to be spawned and for how many times
/// </summary>

[Serializable]
public class SpawnRequest
{

    public GameObject spawnObject;
    public int amount;

    public SpawnRequest(GameObject spawnObject, int amount)
    {
        this.spawnObject = spawnObject;
        this.amount = amount;
    }

    public override bool Equals(object obj)
    {
        //Debug.Log("Equal override");
        var item = obj as SpawnRequest;

        if (item == null)
        {
            return false;
        }

        return spawnObject.Equals(item.spawnObject);
    }
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public int modifyAmount(int a)
    {
        amount += a;
        return amount;
    }

}
