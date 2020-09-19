using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    public List<UnitScript> unitTurns;
    public int unitTurnsPointer = 0;
    public UnitScript currentTurn;
    public TurnHandler turnHandler;

    // Start is called before the first frame update
    void Start()
    {
        currentTurn = unitTurns[unitTurnsPointer];
        turnHandler.setCurrentUnit(currentTurn);
    }

    public void nextTurn()
    {
        unitTurnsPointer++;
        unitTurnsPointer = unitTurnsPointer % unitTurns.Capacity;
        currentTurn = unitTurns[unitTurnsPointer];
        turnHandler.setCurrentUnit(currentTurn);
       
    }
}
