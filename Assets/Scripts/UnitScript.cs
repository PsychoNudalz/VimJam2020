﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitScript : MonoBehaviour
{


    [Header("Components")]
    public Rigidbody2D rb;
    public GameObject aimObject;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public AbilityClassScript abilityClassScript;
    public TextMeshPro healthTextBox;
    public DamagePopUpManagerScript damagePopUpManagerScript;

    [Header("Base States")]
    public bool isPlayer = true;
    public float speed = 5f;
    public int health;
    public int AC = 0;
    public float movement;

    [Header("Weapon")]
    //public int isRange;
    public GameObject mainWeapon;
    public int ammo;
    //public List<GameObject> weaponList;
    public int baseDamage;
    public int toHit;
    public LayerMask layerMask_target;
    public List<string> targetTag;
    public LayerMask layerMask_insight;


    [Header("Current States")]
    public int health_current;
    public float movement_current;
    public List<UnitScript> targetUnits;
    public Vector2 targetPosition;

    [Header("Ranges")]
    public float attackRange = 1;
    public float abilityRange = 1;
    public float interactionRange = 2;

    [Header("Action counter")]
    public int actionMax = 1;
    public int actionCount;
    public int interactionMax = 1;
    public int interactionCount;



    [Header("Other")]
    public Vector2 moveDirection;
    public Vector3 locationLastFrame;

    // Start is called before the first frame update
    private void Awake()
    {
        damagePopUpManagerScript = GameObject.FindObjectOfType<DamagePopUpManagerScript>();
    }


    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        health_current = health;
        movement_current = movement;
        displayCurrentHealth();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (moveDirection.magnitude > 0.1f && isPlayer)
        {
            move();
            locationLastFrame = transform.position;

        }

        try
        {

            if (spriteRenderer.material.GetFloat("_Outline") > 0.01f)
            {
                spriteRenderer.material.SetFloat("_Outline", spriteRenderer.material.GetFloat("_Outline") - Time.fixedDeltaTime * 2f);
            }
        }
        catch (System.Exception e)
        {

        }


    }

    //Turn control

    public void endTurn()
    {
        stopMove();
        disableAimObject();
    }

    public void newTurn()
    {
        displayCurrentHealth();
        moveDirection = new Vector2();
        targetUnits = new List<UnitScript>();

        if (health_current > 0)
        {

            movement_current = movement;
            interactionCount = interactionMax;
            actionCount = actionMax;
            locationLastFrame = transform.position;

        }
    }

    public void changeState()
    {
        stopMove();
    }

    //Movement
    public virtual void moveUnit(Vector2 dir)
    {
        moveDirection = dir;
        animator.SetFloat("Speed", moveDirection.magnitude);
        animator.SetFloat("H_Speed", moveDirection.x);

    }

    public virtual void move()
    {
        if (movement_current > 0)
        {

            animator.SetFloat("Speed", moveDirection.magnitude);
            animator.SetFloat("H_Speed", moveDirection.x);

            rb.MovePosition(rb.position + moveDirection * speed * Time.fixedDeltaTime);
            movement_current -= (transform.position - locationLastFrame).magnitude;
        }
        else
        {
            stopMove();
        }
    }

    public void stopMove()
    {
        moveDirection = new Vector2();

        animator.SetFloat("Speed", 0);
        animator.SetFloat("H_Speed", 0);
    }



    //Attack
    public virtual void weaponAttack()
    {
        if (ammo > 0)
        {

            GameObject tempWeapon = Instantiate(mainWeapon, transform.position, transform.rotation);
            tempWeapon.GetComponent<WeaponScript>().attack(targetPosition, toHit, baseDamage);
            //weaponList.Add(tempWeapon);
            disableAimObject();
            animator.SetTrigger("Attack");
            highlightTargets_Off();
            ammo--;
            actionCount--;
        }
    }


    public void updateAimPoint(Vector3 pos)
    {
        pos.z = 0;
        aimObject.SetActive(true);
        //print((pos - transform.position).magnitude);
        if ((pos - transform.position).magnitude > attackRange)
        {
            targetPosition = (pos - transform.position).normalized * attackRange + transform.position;
        }
        else
        {
            //print("pos ion range");
            targetPosition = pos;
        }

        aimObject.transform.position = targetPosition;
        updateTargets(targetTag);
    }


    void updateTargets(List<string> targetList)
    {
        highlightTargets_Off();
        targetUnits = new List<UnitScript>();
        Vector2 dis = (targetPosition - (Vector2)transform.position);
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, dis.normalized, dis.magnitude, layerMask_target);

        foreach (RaycastHit2D h in hits)
        {
            if (targetList.Contains(h.collider.tag))
            {
                targetUnits.Add(h.collider.GetComponent<UnitScript>());
            }
        }
        highlightTargets_On();
    }

    public void disableAimObject()
    {
        aimObject.SetActive(false);

    }

    //Ability
    public void useAbility()
    {
        ammo += abilityClassScript.useAbility(abilityRange);
        actionCount--;

    }

    //Pick up loot
    public void pickUpLoot()
    {
        List<LootPickupScript> effectedList = new List<LootPickupScript>();
        RaycastHit2D[] tempGO = Physics2D.CircleCastAll(transform.position, interactionRange, new Vector2(), 0);
        RaycastHit2D[] tempRay;
        foreach (RaycastHit2D r in tempGO)
        {
            if (r.collider.CompareTag("Loot") && r.collider.TryGetComponent<LootPickupScript>(out LootPickupScript w))
            {
                tempRay = Physics2D.RaycastAll(w.transform.position, transform.position - w.transform.position, interactionRange, layerMask_insight);
                print(tempRay.Length);
                if (tempRay.Length == 0)
                {
                    effectedList.Add(w);
                }
            }
        }

        foreach (LootPickupScript l in effectedList)
        {
            l.moveToLocation(transform.position);
        }

        if (effectedList.Count > 0)
        {
            interactionCount--;
        }

        return;
    }





    //Highlight
    void highlightTargets_On()
    {
        foreach (UnitScript t in targetUnits)
        {
            t.OutlineSelf_On();
        }
    }
    void highlightTargets_Off()
    {
        foreach (UnitScript t in targetUnits)
        {
            t.OutlineSelf_Off();
        }
    }

    public void OutlineSelf_On()
    {
        spriteRenderer.material.SetFloat("_Outline", 1);
    }
    public void OutlineSelf_Off()
    {
        spriteRenderer.material.SetFloat("_Outline", 0);
    }




    //Damage
    public void takeDamage(int damage)
    {
        displayDamage(damage);
        health_current -= damage;
        if (health_current <= 0)
        {
            die();

        }
        displayCurrentHealth();
        print(name + " damge " + damage + " HP " + health_current);
    }

    void displayDamage(int damage)
    {
        StartCoroutine(takeDamageHighLight());
        float randomPos = 0.2f;
        if (damage == 0)
        {
            damagePopUpManagerScript.newDamageText("MISS", transform.position + new Vector3(Random.Range(-randomPos, randomPos), Random.Range(-randomPos, randomPos)), Color.red);
        }
        else
        {
            damage = -damage;
            damagePopUpManagerScript.newDamageText(damage.ToString(), transform.position + new Vector3(Random.Range(-randomPos, randomPos), Random.Range(-randomPos, randomPos)), Color.red);

        }
    }

    IEnumerator takeDamageHighLight()
    {
        OutlineSelf_On();
        yield return new WaitForSeconds(0.5f);
        OutlineSelf_Off();
    }

    void die()
    {
        health_current = 0;
        animator.SetBool("Dead", true);
        if (!isPlayer)
        {
            Destroy(gameObject, animator.GetCurrentAnimatorClipInfo(0).Length);

        }
    }

    public bool isDead()
    {
        return health_current <= 0;
    }

    //Range getters
    public float getRange_Attack()
    {
        return attackRange;
    }
    public float getRange_Ability()
    {
        return abilityRange;
    }
    public float getRange_Interaction()
    {
        return interactionRange;
    }
    public float getRange_Movement()
    {
        return movement_current;
    }


    //Get Action and Interaction count
    public bool canAction()
    {
        return actionCount > 0;
    }
    public bool canInteraction()
    {
        return interactionCount > 0;
    }
    public bool canMove()
    {
        return movement_current > 0;
    }

    //Health Display
    public void displayCurrentHealth()
    {
        healthTextBox.text = health_current + "/" + health;
    }
}
