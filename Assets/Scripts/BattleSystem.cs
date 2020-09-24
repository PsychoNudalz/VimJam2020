using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    [Header("Turn")]
    public List<UnitScript> turnOrder;
    public int turnOrderPointer = 0;
    public UnitScript currentTurn;
    public TurnHandler turnHandler;
    [Header("Camera")]
    public Transform cameraFocusPoint;
    public Cinemachine.CinemachineVirtualCamera cinemachine;
    [Header("UI")]
    public ActionBarScript actionBarScript;
    public BattleUIScript battleUIScript;
    public UIHandlerScript uIHandlerScript;

    // Start is called before the first frame update
    void Start()
    {
        currentTurn = turnOrder[turnOrderPointer];
        turnHandler.setCurrentUnit(currentTurn);
        actionBarScript = GameObject.FindObjectOfType<ActionBarScript>();
        if(actionBarScript == null)
        {
            actionBarScript = FindObjectOfType<ActionBarScript>();
        }
        if(cinemachine == null)
        {
            cinemachine = FindObjectOfType<Cinemachine.CinemachineVirtualCamera>();
        }
        if (battleUIScript == null)
        {
            battleUIScript = FindObjectOfType<BattleUIScript>();
        }
        if (uIHandlerScript == null)
        {
            uIHandlerScript = FindObjectOfType<UIHandlerScript>();
        }
        UIUpdate();
    }


    private void Update()
    {
        if (currentTurn == null)
        {
            nextTurn();
        }
        else
        {
            focusPointFollow(currentTurn.transform.position);

        }

    }


    //turn Control
    public void nextTurn()
    {
        if (turnOrder.Capacity == 0)
        {
            return;
        }
        cleanTurnOrder();
        turnOrderPointer++;
        turnOrderPointer = turnOrderPointer % turnOrder.Count;
        currentTurn = turnOrder[turnOrderPointer];
        if (currentTurn == null)
        {
            turnOrder.RemoveAt(turnOrderPointer);
            turnOrderPointer = turnOrderPointer % turnOrder.Count;
            currentTurn = turnOrder[turnOrderPointer];
        }

        turnHandler.setCurrentUnit(currentTurn);
        if (!currentTurn.isPlayer)
        {
            if (currentTurn.TryGetComponent<AIUnitScript>(out AIUnitScript aIUnitScript))
            {
                aIUnitScript.startAI();

            }

        }
        actionBarScript.resetBar();

        UIUpdate();
    }

    public void addTurn(UnitScript u)
    {
        cleanTurnOrder();
        if (!turnOrder.Contains(u))
        {
            turnOrder.Add(u);
        }
    }


    void cleanTurnOrder()
    {
        List<UnitScript> temp = new List<UnitScript>();
        foreach(UnitScript u in turnOrder)
        {
            if (u != null)
            {
                temp.Add(u);
            }
        }
        turnOrder = temp;
    }

    //Camera control

    public void moveCamera(Vector3 position)
    {
        cameraFocusPoint.transform.position = position;
    }

    public void focusCamera(GameObject g)
    {
        cameraFocusPoint.transform.position = g.transform.position;
    }

    public void focusPointFollow(Vector3 position)
    {
        cameraFocusPoint.position = position;
    }

    public void resetCameraToPoint()
    {
        cinemachine.Follow = cameraFocusPoint;
    }

    public void setCameraLockAt(Transform t)
    {
        cinemachine.Follow = t;
    }

    //UI
    public void UIUpdate()
    {
        print("Update UI");

        //Turn Order
        List<UnitScript> nextFewTurns = new List<UnitScript>();
        for(int i = 0; i < 7; i++)
        {
            nextFewTurns.Add(turnOrder[(turnOrderPointer + i) % turnOrder.Count]);
        }

        battleUIScript.updateTurnOrder(nextFewTurns);
    }

    public void showGameOver()
    {
        uIHandlerScript.showGameOver();
    }
}
