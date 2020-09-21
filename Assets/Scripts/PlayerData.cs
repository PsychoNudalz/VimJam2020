﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int money;

    public PlayerData(PlayerManagerScript player)
    {
        money = player.money;
    }
}
