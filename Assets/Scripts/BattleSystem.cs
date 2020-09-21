using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    public List<UnitScript> unitTurns;
    public int unitTurnsPointer = 0;
    public UnitScript currentTurn;
    public TurnHandler turnHandler;
    public Transform cameraFocusPoint;
    public ActionBarScript actionBarScript;
    public Cinemachine.CinemachineVirtualCamera cinemachine;

    // Start is called before the first frame update
    void Start()
    {
        currentTurn = unitTurns[unitTurnsPointer];
        turnHandler.setCurrentUnit(currentTurn);
        actionBarScript = GameObject.FindObjectOfType<ActionBarScript>();
    }


    private void Update()
    {
        focusPointFollow(currentTurn.transform.position);
    }

    public void nextTurn()
    {
        if (unitTurns.Capacity == 0)
        {
            return;
        }
        unitTurnsPointer++;
        unitTurnsPointer = unitTurnsPointer % unitTurns.Capacity;
        currentTurn = unitTurns[unitTurnsPointer];
        if (currentTurn == null)
        {
            unitTurns.RemoveAt(unitTurnsPointer);
            unitTurnsPointer--;
            nextTurn();
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
        //setCameraLockAt(currentTurn.transform);

    }

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
}
