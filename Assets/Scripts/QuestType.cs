using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
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

    public override string ToString()
    {
        string tempS = getObjectiveDescription() + "\n\n" +
            "Potential Number Of Enemies: " + potentialNumberOfEnemies() + "\n\n" +
            "Max Enemy Level: "+maxCost;
        return tempS;
    }

    public  string getObjectiveDescription()
    {
        string tempS = "Objective: ";
        switch (objectiveEnum)
        {
            case (ObjectiveEnum.COLLECTION):
                tempS += " Collect Loot Pieces\n0/" + targetValue;
                break;
        }
        return tempS;
    }

    public int potentialNumberOfEnemies()
    {
        return Mathf.FloorToInt(totalEnemyCost / Mathf.RoundToInt(maxCost+.2f / 2f));
    }

    public float getQuestScore()
    {
        return (totalEnemyCost * targetValue) / 10f;
    }
}
