using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class TurnHandler : MonoBehaviour
{
    public InputMaster controls;
    public Camera camera;
    public UnitScript currentUnit;
    public TurnEnum currentState = TurnEnum.NONE;


    // Start is called before the first frame update
    private void Awake()
    {
        controls = new InputMaster();
        controls.Player.Movement.performed += ctx => moveCurrentUnit(ctx.ReadValue<Vector2>());
        controls.Player.Movement.canceled += ctx => moveCurrentUnit(new Vector2());

    }

    // Update is called once per frame
    void Update()
    {
        //print(controls.Player.Movement.enabled);
        switch (currentState)
        {
            case TurnEnum.ACTION:
                break;
        }
    }
    public void setCurrentUnit(UnitScript unit)
    {
        if (currentUnit != null)
        {

            currentUnit.endTurn();
        }


        currentUnit = unit;
        currentUnit.newTurn();
    }

    public void setCurrentState_Action()
    {
        currentState = TurnEnum.ACTION;
        Debug.Log(currentUnit.name + " selected state Action");
    }

    public void setCurrentState_Interation()
    {
        currentState = TurnEnum.INTERACTION;
        Debug.Log(currentUnit.name + " selected state Interaction");

    }
    public void setCurrentState_Movement()
    {
        currentState = TurnEnum.MOVEMENT;
        Debug.Log(currentUnit.name + " selected state Movement");

    }
    public void setCurrentState_None()
    {
        currentState = TurnEnum.NONE;
        Debug.Log(currentUnit.name + " selected state None");

    }




    void moveCurrentUnit(Vector2 dir)
    {
        if (currentState.Equals(TurnEnum.MOVEMENT))
        {
            //print("test");
            currentUnit.moveUnit(dir);
        }
    }

    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }

}
