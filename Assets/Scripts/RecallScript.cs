using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecallScript : InteractableObjectScript
{
    public QuestManager questManager;
    

    private void Awake()
    {
        questManager = FindObjectOfType<QuestManager>();
    }

    public override bool activeObject()
    {

        print("Attempt Recall");
        if (questManager.checkPlayerComplete())
        {
            print("Player Complete");
            //Debug
            FindObjectOfType<UIHandlerScript>().toggle_upgradeMode();
            return true;
        }
        return false;
    }
}
