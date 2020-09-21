using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBarScript : MonoBehaviour
{
    public TurnHandler turnHandler;
    public BattleSystem battleSystem;
    public List<GameObject> mainButtons;
    public GameObject actionMenu;
    public GameObject interactionMenu;


    private void Awake()
    {
        turnHandler = GameObject.FindObjectOfType<TurnHandler>();
        battleSystem = GameObject.FindObjectOfType<BattleSystem>();
    }


    public void setCurrentState_Action()
    {
        resetBar();
        turnHandler.setCurrentState_Action();
        setActionMenu_Active();
    }

    public void setCurrentState_Interation()
    {
        resetBar();
        turnHandler.setCurrentState_Interation();
        setInteractionMenu_Active();

    }
    public void setCurrentState_Movement()
    {
        resetBar();
        turnHandler.setCurrentState_Movement();

    }
    public void setCurrentState_None()
    {
        resetBar();
        turnHandler.setCurrentState_None();

    }

    public void setActionMenu_Active()
    {
        actionMenu.SetActive(true);

    }

    public void setInteractionMenu_Active()
    {
        interactionMenu.SetActive(true);

    }


    public void currentUnit_Attack()
    {
        turnHandler.currentUnit_Attack();
        resetBar();
    }

    public void currentUnit_Ability()
    {
        turnHandler.currentUnit_Ability();
        resetBar();
    }

    public void currentUnit_PickUp()
    {
        turnHandler.currentUnit_PickUp();
        resetBar();
    }

    public void currentUnit_Use()
    {
        resetBar();
    }




    public void resetBar()
    {
        actionMenu.SetActive(false);
        interactionMenu.SetActive(false);
        if (!turnHandler.currentUnit.isPlayer)
        {
            disableBarButton();
        }
        else
        {
            resetBarButtons();
        }

    }

    public void endTurn()
    {
        setCurrentState_None();
        battleSystem.nextTurn();
        resetBar();
    }

    public void resetBarButtons()
    {
        if (turnHandler.canAction())
        {
            mainButtons[0].SetActive(true);
        }
        else
        {
            mainButtons[0].SetActive(false);
        }
        if (turnHandler.canInteraction())
        {
            mainButtons[1].SetActive(true);
        }
        else
        {
            mainButtons[1].SetActive(false);
        }
        if (turnHandler.canMove())
        {
            mainButtons[2].SetActive(true);
        }
        else
        {
            mainButtons[2].SetActive(false);
        }
        mainButtons[3].SetActive(true);
    }

    public void disableBarButton()
    {
        foreach(GameObject g in mainButtons)
        {
            g.SetActive(false);
        }
    }
}
