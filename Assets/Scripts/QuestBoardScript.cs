using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestBoardScript : SceneControlScript
{
    [Header("Player")]
    public PlayerManagerScript playerManager;

    [Header("Quest")]
    public List<QuestPieceScript> questPieceScripts = new List<QuestPieceScript>();
    public QuestType selectedQuest;

    [Header("Text")]
    public TextMeshProUGUI selectedQuestText;
    public TextMeshProUGUI difficultyQuestText;
    public TextMeshProUGUI partyText;

    [Header("Secret Mission")]
    public string code;
    public GameObject secret;

    private void Awake()
    {

        if (playerManager == null)
        {
            playerManager = FindObjectOfType<PlayerManagerScript>();
        }
        if (questPieceScripts.Count == 0)
        {
            questPieceScripts = new List<QuestPieceScript>(GetComponentsInChildren<QuestPieceScript>());
        }
        generateQuests();
        selectedQuest = questPieceScripts[0].questType;
    }

    private void FixedUpdate()
    {
        if (selectedQuest != null)
        {
            displayQuest();
            displayDifficulty();
        }
        if (playerManager == null)
        {
            playerManager = FindObjectOfType<PlayerManagerScript>();
        }
        displayParty();
        playerManager.questType = selectedQuest;

    }

    public void generateQuests()
    {
        foreach (QuestPieceScript q in questPieceScripts)
        {
            q.generateQuest();
        }
    }


    //Display
    public void displayQuest()
    {
        selectedQuestText.text = selectedQuest.ToString();
    }

    public (string, Color) calculateDifficulty()
    {

        string tempS = "";
        if (selectedQuest == null)
        {
            return ("-----", Color.white);
        }

        float sum = selectedQuest.getQuestScore() - playerManager.getAverageLevel();

        if (sum > 100)
        {
            return (tempS + "DEADLY!", Color.black);
        }
        else if (sum > 50)
        {
            return (tempS + "POSSIBLE?", Color.red);
        }
        else if (sum > 0)
        {
            return (tempS + "DOABLE", Color.yellow);
        }
        else
        {
            return (tempS + "PICE OF CAKE!", Color.green);
        }

    }

    public void displayDifficulty()
    {
        difficultyQuestText.text = calculateDifficulty().Item1;
        difficultyQuestText.color = calculateDifficulty().Item2;
    }

    public void displayParty()
    {
        partyText.text = playerManager.ToString();
    }

    //Button
    public void startLevel()
    {
        playerManager.activateUnits();
        loadScene(2);
    }

    public void addCode(string i)
    {
        code += i;
        if (code.Contains("uuddlrlrba"))
        {
            secret.SetActive(true);
        }
        
    }
}
