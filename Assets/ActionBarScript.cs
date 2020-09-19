using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBarScript : MonoBehaviour
{
    public TurnHandler turnHandler;
    public BattleSystem battleSystem;


    private void Awake()
    {
        turnHandler = GameObject.FindObjectOfType<TurnHandler>();
        battleSystem = GameObject.FindObjectOfType<BattleSystem>();
    }


    public void setCurrentState_Action()
    {
        turnHandler.setCurrentState_Action();
    }

    public void setCurrentState_Interation()
    {
        turnHandler.setCurrentState_Interation();

    }
    public void setCurrentState_Movement()
    {
        turnHandler.setCurrentState_Movement();

    }
    public void setCurrentState_None()
    {
        turnHandler.setCurrentState_None();

    }

    public void endTurn()
    {
        setCurrentState_None();
        battleSystem.nextTurn();
        
    }
}
