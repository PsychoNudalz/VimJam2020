using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScript : MonoBehaviour
{
   

    [Header("Components")]
    public Rigidbody2D rb;
    public GameObject moveCircle;
    public Animator animator;

    [Header("Base States")]
    public bool isPlayer = true;
    public float speed = 5f;
    public int health;
    public float movement;

    [Header("Current States")]
    public int health_current;
    public float movement_current;

    [Header("Other")]
    public Vector2 moveDirection;
    // Start is called before the first frame update
    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        health_current = health;
        movement_current = movement;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (moveDirection.magnitude > 0.1f)
        {
            move();
        }

    }

    public void moveUnit(Vector2 dir)
    {
        moveDirection = dir;
        animator.SetFloat("Speed", moveDirection.magnitude);
        animator.SetFloat("H_Speed", moveDirection.x);

    }

    private void move()
    {
        if (movement_current > 0.1f)
        {

            animator.SetFloat("Speed", moveDirection.magnitude);
            animator.SetFloat("H_Speed", moveDirection.x);

            rb.MovePosition(rb.position + moveDirection * speed * Time.fixedDeltaTime);
            movement_current -= speed * Time.fixedDeltaTime;
            updateMoveRange(movement_current);
        }
        else
        {
            updateMoveRange(0);
            animator.SetFloat("Speed", 0);
            animator.SetFloat("H_Speed", 0);


        }
    }

    public void endTurn()
    {
        moveCircle.SetActive(false);

    }

    public void newTurn()
    {
        moveCircle.SetActive(true);
        movement_current = movement;
        updateMoveRange(movement_current);

    }

    public void updateMoveRange(float size)
    {
        moveCircle.transform.localScale = new Vector3(size * 10, size * 10, 1);
    }
}
