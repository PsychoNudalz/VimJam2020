using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class TurnHandler : MonoBehaviour
{
    public InputMaster controls;
    private Mouse mouse;
    public Camera camera;
    public GameObject rangeCircle;

    public UnitScript currentUnit;
    public TurnEnum currentState = TurnEnum.NONE;
    [SerializeField] bool aiming = false;


    // Start is called before the first frame update
    private void Awake()
    {
        mouse = InputSystem.GetDevice<Mouse>();
        controls = new InputMaster();
        controls.Player.Aim.performed += _ => setAiming(true);
        controls.Player.Aim.canceled += _ => setAiming(false);

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
                if (aiming)
                {
                    selectLocation();
                }
                break;
        }
        displayRange();
    }
    public void setCurrentUnit(UnitScript unit)
    {
        if (currentUnit != null)
        {

            currentUnit.endTurn();
        }


        currentUnit = unit;

        currentUnit.newTurn();
        currentUnit.isTurn = true;
    }

    public void setCurrentState_Action()
    {
        currentState = TurnEnum.ACTION;
        currentUnit.changeState();
        Debug.Log(currentUnit.name + " selected state Action");
    }

    public void setCurrentState_Interation()
    {
        currentUnit.disableAimObject();
        currentState = TurnEnum.INTERACTION;
        currentUnit.changeState();

        Debug.Log(currentUnit.name + " selected state Interaction");

    }
    public void setCurrentState_Movement()
    {
        currentUnit.disableAimObject();
        currentState = TurnEnum.MOVEMENT;
        currentUnit.changeState();
        Debug.Log(currentUnit.name + " selected state Movement");

    }
    public void setCurrentState_None()
    {
        currentState = TurnEnum.NONE;
        currentUnit.changeState();
        Debug.Log(currentUnit.name + " selected state None");

    }

    public void currentUnit_Attack()
    {
        currentUnit.weaponAttack();
    }

    public void currentUnit_Ability()
    {
        currentUnit.useAbility();
    }

    public void currentUnit_PickUp()
    {
        currentUnit.pickUpLoot();
    }



    void moveCurrentUnit(Vector2 dir)
    {
        if (currentState.Equals(TurnEnum.MOVEMENT))
        {
            //print("test");
            currentUnit.moveUnit(dir);
        }
    }

    void setAiming(bool b)
    {
        aiming = b;
    }

    void selectLocation()
    {
        if (currentState.Equals(TurnEnum.ACTION))
        {
            //currentUnit.targetPosition = ;
            currentUnit.updateAimPoint(camera.ScreenToWorldPoint(mouse.position.ReadValue()));

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



    public void displayRange()
    {
        switch (currentState)
        {
            case (TurnEnum.ACTION):
                updateRange(currentUnit.getRange_Attack());
                break;
            case (TurnEnum.INTERACTION):
                updateRange(currentUnit.getRange_Interaction());
                break;
            case (TurnEnum.MOVEMENT):
                updateRange(currentUnit.getRange_Movement());
                break;
            case (TurnEnum.NONE):
                updateRange(0);
                break;

        }
    }
    public void updateRange(float size)
    {
        if (currentUnit != null)
        {

            rangeCircle.transform.localScale = new Vector3(size * 10, size * 10, 1);
            rangeCircle.transform.position = currentUnit.transform.position;
        }
    }


    //Get Action and Interaction count
    public bool canAction()
    {
        return currentUnit.canAction();
    }
    public bool canInteraction()
    {
        return currentUnit.canInteraction();
    }
    public bool canMove()
    {
        return currentUnit.canMove();
    }
}
