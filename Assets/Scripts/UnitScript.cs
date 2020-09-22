using System.Collections;
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
    public string unitName;
    public bool isPlayer = true;
    public float speed = 5f;
    public int health;
    public int AC = 0;
    public float movement;

    [Header("Weapon")]
    //public int isRange;
    public GameObject mainWeapon;
    public int ammo;
    public int baseDamage;
    public int toHit;
    public int weaponDamage;
    public LayerMask layerMask_target;
    public List<string> targetTag;
    public LayerMask layerMask_insight;

    [Header("Ranges")]
    public float attackRange = 1;
    public float abilityRange = 1;
    public float interactionRange = 2;

    [Header("Current States")]
    public int health_current;
    public float movement_current;
    public List<UnitScript> targetUnits;
    public Vector2 targetPosition;


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

    public virtual void endTurn()
    {
        stopMove();
        disableAimObject();
        highlightTargets_Off();
    }

    public virtual void newTurn()
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
        //abilityClassScript.getEffectedList(abilityRange, layerMask_insight);
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
            tempWeapon.GetComponent<WeaponScript>().attack(targetPosition, toHit, baseDamage, weaponDamage);
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
        Vector3 dir = pos - transform.position;
        if (dir.magnitude > attackRange)
        {
            targetPosition = dir.normalized * attackRange + transform.position;
        }
        else
        {
            targetPosition = pos;
        }
        dir = (Vector3)targetPosition - transform.position;

        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, dir.normalized, dir.magnitude, layerMask_insight);
        if (hit)
        {
            targetPosition = hit.point;
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
        int amount = abilityClassScript.useAbility(abilityRange, layerMask_insight);
        if (amount != 0)
        {
            ammo += amount;
            actionCount--;
        }


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
                //print(tempRay.Length);
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
            if (t != null && t.gameObject.activeSelf)
            {
                t.OutlineSelf_On();

            }
        }
    }
    void highlightTargets_Off()
    {
        foreach (UnitScript t in targetUnits)
        {
            if (t != null && t.gameObject.activeSelf)
            {
                t.OutlineSelf_Off();
            }
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
    public void takeDamage(int damage, int rollToHit)
    {
        if (rollToHit < AC)
        {
            damage = 0;
        }

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
        float randomPos = 0.7f;
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

    public virtual void die()
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


    //Converts States to saveData
    public int[] statesToSaveData()
    {
        /*
          
        [Header("Base States")]
        public int health;
        public int AC = 0;
        public float movement;

        [Header("Weapon")]
        public int ammo;
        public int baseDamage;
        public int toHit;
        public int weaponDamage;


        [Header("Action counter")]
        public int actionMax = 1;
        public int interactionMax = 1;
        */

        int[] returnStates = { health, AC, Mathf.FloorToInt(movement), ammo, baseDamage, toHit, weaponDamage, actionMax, interactionMax };

        return returnStates;

    }


    public bool saveDataToStates(int[] saveData)
    {
        try
        {
            health = saveData[0];
            AC = saveData[1];
            movement = saveData[2];
            ammo = saveData[3];
            baseDamage = saveData[4];
            toHit = saveData[5];
            weaponDamage = saveData[6];
            actionMax = saveData[7];
            interactionMax = saveData[8];
            return true;
        } catch (System.Exception e)
        {
            Debug.LogError(name + " failed to load data");
            return false;
        }
    }


    //States To Display

    public string ToString_Health()
    {
        string t = health.ToString();
        return t;
    }
    public string ToString_AC()
    {
        string t = AC.ToString();
        return t;
    }
    public string ToString_Moevement()
    {
        string t = (movement*5).ToString()+"ft";
        return t;
    }
    public virtual string ToString_Ability()
    {
        string t = "Ability";
        return t;
    }
    public string ToString_Damage()
    {
        string t = "1d" +weaponDamage.ToString();
        return t;
    }
    public string ToString_ToHit()
    {
        string t ="+"+toHit;
        return t;
    }

    public Sprite ToDisplay_Character()
    {
        return spriteRenderer.sprite;
    }

    public Sprite ToDisplay_Weapon()
    {
        return mainWeapon.GetComponentInChildren<SpriteRenderer>().sprite;
    }
}
