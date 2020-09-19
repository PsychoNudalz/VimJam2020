using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacterScript : MonoBehaviour
{
    
    public InputMaster controls;
    public Rigidbody2D rb;
    [Header("Character States")]
    public float speed = 3;
    [Header("Movement")]
    public Vector2 moveDirection = new Vector2();

    private void Awake()
    {
        controls = new InputMaster();

        controls.Player.Select.performed += _ => TestFunction("Select");
        controls.Player.Movement.performed += ctx => Move(ctx.ReadValue<Vector2>());
        controls.Player.Movement.canceled += ctx => Move(new Vector2());

    }


    // Update is called once per frame
    void FixedUpdate()
    {

    }

    void Move(Vector2 direction)
    {
        Debug.Log("Move to " + direction);
        moveDirection = direction;
    }

    void TestFunction(string str)
    {
        Debug.Log(str);
    }


}
