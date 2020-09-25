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
    public int minCost = 0;

    public QuestType(ObjectiveEnum objectiveEnum, int targetValue, int totalEnemyCost, int waveAmount, int maxCost, int minCost = 0)
    {
        this.objectiveEnum = objectiveEnum;
        this.targetValue = targetValue;
        this.totalEnemyCost = totalEnemyCost;
        this.waveAmount = waveAmount;
        this.maxCost = maxCost;
        this.minCost = minCost;
    }

    public override string ToString()
    {
        string tempS = getObjectiveDescription() + "\n\n" +
            "Potential Number Of Enemies: " + potentialNumberOfEnemies()+"-"+totalEnemyCost + "\n\n" +
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
    public int potentialMaxNumberOfEnemies()
    {
        return Mathf.FloorToInt(totalEnemyCost / Mathf.RoundToInt(minCost + .2f / 2f));
    }

    public float getQuestScore()
    {
        return (totalEnemyCost * targetValue) / 10f;
    }
}
