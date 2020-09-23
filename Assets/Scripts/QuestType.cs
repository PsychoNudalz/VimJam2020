using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestType 
{
    public ObjectiveEnum objectiveEnum = ObjectiveEnum.COLLECTION;
    public int targetValue = 3;
    public int totalEnemyCost = 10;
    public int waveAmount = 3;
    public int maxCost = 3;

    public QuestType(ObjectiveEnum objectiveEnum, int targetValue, int totalEnemyCost, int waveAmount, int maxCost)
    {
        this.objectiveEnum = objectiveEnum;
        this.targetValue = targetValue;
        this.totalEnemyCost = totalEnemyCost;
        this.waveAmount = waveAmount;
        this.maxCost = maxCost;
    }
}
