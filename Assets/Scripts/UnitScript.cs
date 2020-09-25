using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitScript : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D rb;
    public GameObject aimObject_Attack;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public AbilityClassScript abilityClassScript;
    public TextMeshPro healthTextBox;
    public TextMeshPro ammoTextBox;
    public DamagePopUpManagerScript damagePopUpManagerScript;

    [Header("Base States")]
    public string unitName;
    public int level;
    public bool isPlayer = true;
    public float speed = 5f;
    public int health;
    public int AC = 0;
    public float movement;
    public int abilityLevel = 4;

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
    public int ammo_Current;
    public int temp_AC = 0;
    public List<UnitScript> targetUnits;
    public Vector2 targetPosition;

    [Header("Interaction")]
    public LayerMask layerMask_interactable;
    public InteractableObjectScript interactTarget;
    public GameObject aimObject_Interactable;
    //public List<InteractableObjectScript> targetInteractable;


    [Header("Action counter")]
    public int actionMax = 1;
    public int actionCount;
    public int interactionMax = 1;
    public int interactionCount;

    [Header("Upgrade Values")]
    public int upgrade_Health = 5;
    public int upgrade_AC = 1;
    public int upgrade_Movement = 1;
    public int upgrade_AttackBonus = 1;
    public int upgrade_WeaponDamage = 2;
    public int upgrade_Ability = 1;

    [Header("Sounds")]
    public SoundManager soundManager;
    public Sound sound_Move;
    public Sound sound_Hit;
    public Sound sound_Ability;
    public Sound sound_Attack;

    [Header("Other")]
    public bool isTurn = false;
    public Vector2 moveDirection;
    public Vector3 locationLastFrame;
    [Header("health, AC, speed, ammo, bDmg, toHit, wDmg, a, i, Lv. ab")]
    //{ health, AC, movement, ammo, baseDamage, toHit, weaponDamage, actionMax, interactionMax, level };
    public int[] BASESTATS;

    // Start is called before the first frame update
    private void Awake()
    {
        damagePopUpManagerScript = GameObject.FindObjectOfType<DamagePopUpManagerScript>();
        soundManager = GameObject.FindObjectOfType<SoundManager>();
    }


    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        soundManager = GameObject.FindObjectOfType<SoundManager>();

        resetCurrentStats();
        displayCurrentStates();
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
        catch (System.Exception _)
        {

        }

        if (isTurn)
        {
            showEffectedList();
            displayCurrentStates();
        }

        if(soundManager == null)
        {
            soundManager = FindObjectOfType<SoundManager>();
        }
        if (damagePopUpManagerScript == null)
        {
            damagePopUpManagerScript = FindObjectOfType<DamagePopUpManagerScript>();
        }

    }

    //Turn control

    public void resetCurrentStats()
    {
        endTurn();
        health_current = health;
        movement_current = movement;
        ammo_Current = ammo;
        displayCurrentStates();
        moveDirection = new Vector2();
        targetUnits = new List<UnitScript>();
        temp_AC = 0;
        if (health_current > 0)
        {

            movement_current = movement;
            interactionCount = interactionMax;
            actionCount = actionMax;
            locationLastFrame = transform.position;

        }
        //newTurn();
    }


    public virtual void endTurn()
    {
        stopMove();
        disableAimObject_Attack();
        disableAimObject_Interactable();
        highlightTargets_Off();
        isTurn = false;
    }

    public virtual void newTurn()
    {
        isTurn = true;
        displayCurrentStates();
        moveDirection = new Vector2();
        targetUnits = new List<UnitScript>();
        temp_AC = 0;
        if (health_current > 0)
        {

            movement_current = movement;
            interactionCount = interactionMax;
            actionCount = actionMax;
            locationLastFrame = transform.position;

        }
        else
        {
            movement_current = 0;
            interactionCount = 0;
            actionCount = 0;
        }
    }

    public void changeState()
    {
        stopMove();
        isTurn = true;
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


    public void dash()
    {
        if (canAction())
        {
            movement_current += movement;
            actionCount--;
        }
    }



    //Attack
    public virtual void weaponAttack()
    {

        GameObject tempWeapon = Instantiate(mainWeapon, transform.position, transform.rotation);
        tempWeapon.GetComponent<WeaponScript>().attack(targetPosition, toHit, baseDamage, weaponDamage);
        //weaponList.Add(tempWeapon);
        disableAimObject_Attack();
        animator.SetTrigger("Attack");
        highlightTargets_Off();
        actionCount--;
        displayCurrentStates();
        PlaySound_Attack();
    }


    public void updateAimPoint_Attack(Vector3 pos)
    {
        pos.z = 0;
        aimObject_Attack.SetActive(true);
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

        aimObject_Attack.transform.position = targetPosition;
        updateTargets_Attack(targetTag);
    }


    void updateTargets_Attack(List<string> targetList)
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
        highlightTargets_On(Color.red);
    }

    public void disableAimObject_Attack()
    {
        updateAimPoint_Attack(transform.position);
        aimObject_Attack.SetActive(false);
    }

    //Ability
    public virtual void useAbility()
    {
        animator.SetTrigger("Ability");
        displayCurrentStates();

    }

    public void showEffectedList()
    {
        if (canAction())
        {
            abilityClassScript.displayEffectedList(abilityRange, layerMask_insight);
        }
        else
        {
            abilityClassScript.displayEffectedList(0, layerMask_insight);

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

    //Interact
    public void updateAimPoint_Interactable(Vector3 pos)
    {
        pos.z = 0;
        aimObject_Interactable.SetActive(true);
        Vector3 dir = pos - transform.position;
        if (dir.magnitude > interactionRange)
        {
            targetPosition = dir.normalized * interactionRange + transform.position;
        }
        else
        {
            targetPosition = pos;
        }
        dir = (Vector3)targetPosition - transform.position;



        aimObject_Interactable.transform.position = targetPosition;
        List<string> interactableList = new List<string>();
        interactableList.Add("Interactable");
        updateTargets_Interactable(interactableList);
    }


    void updateTargets_Interactable(List<string> targetList)
    {
        /*
        foreach(InteractableObjectScript i in targetInteractable)
        {
            i.setOutline(0f, Color.white);
        }
        targetInteractable = new List<InteractableObjectScript>();
        */
        if (interactTarget != null)
        {
            interactTarget.setOutline(0f, Color.white);

        }
        interactTarget = null;
        Vector2 dis = (targetPosition - (Vector2)transform.position);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dis.normalized, dis.magnitude, layerMask_interactable);

        if (hit)
        {

            interactTarget = hit.collider.GetComponent<InteractableObjectScript>();
            interactTarget.setOutline(1f, Color.white);

        }
        /*
        foreach (RaycastHit2D h in hits)
        {
            if (targetList.Contains(h.collider.tag))
            {
                targetInteractable.Add(h.collider.GetComponent<InteractableObjectScript>());
            }
        }
        foreach (InteractableObjectScript i in targetInteractable)
        {
            i.setOutline(1f, Color.white);
        }
        */
    }

    public void disableAimObject_Interactable()
    {
        updateAimPoint_Interactable(transform.position);
        aimObject_Interactable.SetActive(false);
    }



    public void useInteractable()
    {
        if (interactTarget != null)
        {
            if (interactTarget.activeObject())
            {

                interactionCount--;
            }
        }
    }


    //Highlight
    void highlightTargets_On(Color c)
    {
        foreach (UnitScript t in targetUnits)
        {
            if (t != null && t.gameObject.activeSelf)
            {
                t.OutlineSelf_On(c);
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

    public void OutlineSelf_On(Color c)
    {
        spriteRenderer.material.SetFloat("_Outline", 1);
        spriteRenderer.material.SetColor("_Colour", c);
    }
    public void OutlineSelf_Off()
    {
        spriteRenderer.material.SetFloat("_Outline", 0);


    }




    //Damage
    public void takeDamage(int damage, int rollToHit)
    {
        animator.SetTrigger("TakeDamage");
        PlaySound_Hit();
        if (rollToHit < AC + temp_AC)
        {
            damage = 0;
        }

        health_current -= damage;
        if (health_current <= 0)
        {
            die();

        }
        displayDamage(damage);
        displayCurrentStates();
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
        OutlineSelf_On(Color.red);
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


    //Heal
    public void healUnit(int amount)
    {
        float randomPos = 0.5f;
        health_current = Mathf.Clamp(health_current + amount, 1, health);
        damagePopUpManagerScript.newDamageText("HP+" + amount.ToString(), transform.position + new Vector3(Random.Range(-randomPos, randomPos), Random.Range(-randomPos, randomPos)), Color.green);
        animator.SetBool("Dead", false);
        displayCurrentStates();
    }

    public void tempACBuff(int amount)
    {
        float randomPos = 0.5f;
        temp_AC = amount;
        damagePopUpManagerScript.newDamageText("AC+"+amount.ToString(), transform.position + new Vector3(Random.Range(-randomPos, randomPos), Random.Range(-randomPos, randomPos)), Color.grey);
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
    public void displayCurrentStates()
    {
        healthTextBox.text = "HP:"+ health_current + "/" + health;
        if (ammoTextBox != null)
        {
            ammoTextBox.text = "AP:" + ammo_Current + "/" + ammo;
        }
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


        Level
        */

        int[] returnStates = { health, AC, Mathf.FloorToInt(movement), ammo, baseDamage, toHit, weaponDamage, actionMax, interactionMax, level, abilityLevel };

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
            level = saveData[9];
            abilityLevel = saveData[10];
            return true;
        }
        catch (System.Exception _)
        {
            Debug.LogError(name + " failed to load data");
            return false;
        }
    }

    public void RESET_UNIT()
    {
        saveDataToStates(BASESTATS);
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
        string t = (movement * 5).ToString() + "ft";
        return t;
    }
    public virtual string ToString_Level()
    {
        string t = level.ToString();
        return t;
    }

    public virtual string ToString_Ability()
    {
        string t = "Ability";
        return t;
    }
    public string ToString_Damage()
    {
        string t = "1d" + weaponDamage.ToString();
        return t;
    }
    public string ToString_ToHit()
    {
        string t = "+" + toHit;
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

    //Upgrades

    public int Upgrade_HP()
    {
        health += upgrade_Health;
        level++;
        return health;
    }
    public int Upgrade_AC()
    {
        AC += upgrade_AC;
        level++;
        return AC;
    }

    public int Upgrade_Movement()
    {
        movement += upgrade_Movement;
        level++;
        return Mathf.RoundToInt(movement);
    }
    public int Upgrade_AttackBonus()
    {
        toHit += upgrade_AttackBonus;
        baseDamage += upgrade_AttackBonus;
        level++;
        return toHit;
    }
    public int Upgrade_WeaponDamage()
    {
        weaponDamage += upgrade_WeaponDamage;
        level++;
        return weaponDamage;
    }
    public virtual int Upgrade_Ability()
    {

        return 0;
    }



    //Sound

    public void PlaySound_Move()
    {
        if (sound_Move.source.isPlaying == false)
        {
            print(name + " Play move sound");
            soundManager.Play(sound_Move);
        }
    }

    public void StopSound_Move()
    {
        if (sound_Move.source.isPlaying == true)
        {

            print(name + " stop move sound");
            soundManager.Stop(sound_Move);
        }
    }

    public void PlaySound_Hit()
    {
        soundManager.Play(sound_Hit);
    }
    public void PlaySound_Ability()
    {
        soundManager.Play(sound_Ability);
    }
    public void PlaySound_Attack()
    {
        soundManager.Play(sound_Attack);
    }
}
