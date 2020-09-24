using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public PlayerManagerScript playerManagerScript;
    public BattleSystem battleSystem;

    private void Start()
    {
        if (playerManagerScript == null)
        {
            playerManagerScript = FindObjectOfType<PlayerManagerScript>();
        }
        playerManagerScript.activateUnits();
        if (battleSystem == null)
        {
            battleSystem = FindObjectOfType<BattleSystem>();
        }

        if(playerManagerScript!= null)
        {
        foreach(UnitScript u in playerManagerScript.units)
            {
                battleSystem.addTurn(u);
            }

        }
    }
    private void FixedUpdate()
    {
        if (playerManagerScript.isPlayersDead())
        {
            battleSystem.showGameOver();
        }
    }

}
