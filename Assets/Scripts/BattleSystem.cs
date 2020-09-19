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
    public Cinemachine.CinemachineVirtualCamera cinemachine;

    // Start is called before the first frame update
    void Start()
    {
        currentTurn = unitTurns[unitTurnsPointer];
        turnHandler.setCurrentUnit(currentTurn);
    }


    private void Update()
    {
        focusPointFollow(currentTurn.transform.position);
    }

    public void nextTurn()
    {
        unitTurnsPointer++;
        unitTurnsPointer = unitTurnsPointer % unitTurns.Capacity;
        currentTurn = unitTurns[unitTurnsPointer];
        turnHandler.setCurrentUnit(currentTurn);
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
