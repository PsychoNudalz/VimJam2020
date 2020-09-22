using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int money;
    public int[][] unitStates;

    public PlayerData(PlayerManagerScript player)
    {
        unitStates = new int[player.units.Count][];
        money = player.money;
        for(int i = 0; i < player.units.Count; i++)
        {
            unitStates[i] = player.units[i].statesToSaveData();
        }
    }
}
