using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestPieceScript : MonoBehaviour
{
    [Header("Quest Para")]
    public ObjectiveEnum objectiveEnum = ObjectiveEnum.COLLECTION;
    public Vector2 targetValue = new Vector2(0, 10);
    public Vector2 totalEnemyCost = new Vector2(10, 50);
    public Vector2 waveAmount = new Vector2(1, 10);
    public Vector2 maxCost = new Vector2(1, 6);
    public Vector2 minCost = new Vector2(1, 6);
    public List<Sprite> randomSprite;

    [Header("Component")]
    public QuestType questType = new QuestType(ObjectiveEnum.COLLECTION, 0, 1, 1, 6);
    public QuestBoardScript questBoardScript;

    public void generateQuest()
    {
        GetComponent<Image>().sprite = randomSprite[Mathf.FloorToInt(Random.Range(0, randomSprite.Count - 0.0001f))];

        if (questBoardScript == null)
        {
            questBoardScript = FindObjectOfType<QuestBoardScript>();
        }

        int max = randomValue(maxCost);
        int min = randomValue(minCost);
        if (min > max)
        {
            min = max;
        }
        questType = new QuestType(ObjectiveEnum.COLLECTION, randomValue(targetValue), randomValue(totalEnemyCost), randomValue(waveAmount), max, min);
    }

    int randomValue(Vector2 v)
    {
        return Mathf.RoundToInt(Random.Range(v.x, v.y));
    }

    public void selectQuest()
    {
        questBoardScript.selectedQuest = questType;
    }

    private void OnEnable()
    {
        generateQuest();
    }
}
