using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles Unit's behaviour and stats
/// </summary>
public class AIUnitScript : UnitScript
{
    [Header("AI")]
    public bool activateUnit = false;
    public bool runAI = false;
    public bool isMoving = false;
    public bool pauseAction = false;
    public TurnHandler turnHandler;
    public BattleSystem battleSystem;
    [SerializeField] List<GameObject> targetList;
    public GameObject target;
    public float detectionRange = 50;
    public float endOfTurnWaitTime = 0.5f;
    public float timeBetweenAction = 0.2f;
    public float timeUntilEndTurn = 1f;

    [Header("Loot")]
    public RandomLootSpawnScript randomLootSpawnScript;


    [Header("Pathfinding")]
    public AIPath aIPath;
    public AIDestinationSetter aIDestinationSetter;

    private void Awake()
    {
        damagePopUpManagerScript = GameObject.FindObjectOfType<DamagePopUpManagerScript>();
        turnHandler = GameObject.FindObjectOfType<TurnHandler>();
        battleSystem = GameObject.FindObjectOfType<BattleSystem>();
        randomLootSpawnScript = GetComponent<RandomLootSpawnScript>();
    }

    private void FixedUpdate()
    {
        if (runAI && !pauseAction)
        {
            AIBehavior();
        }
    }

    public void startAI()
    {
        runAI = true;
        if (activateUnit)
        {
            startMoveToTarget();
        }
    }

    IEnumerator endAI()
    {
        runAI = false;
        isMoving = false;
        animator.SetFloat("Speed", 0);
        yield return new WaitForSeconds(endOfTurnWaitTime);
        battleSystem.nextTurn();

    }

    IEnumerator pause()
    {
        pauseAction = true;
        yield return new WaitForSeconds(timeBetweenAction);
        pauseAction = false;
    }
    public override void newTurn()
    {
        base.newTurn();
        timeUntilEndTurn = 2f;
    }

    public void AIBehavior()
    {

        if ((locationLastFrame - transform.position).magnitude < 0.5f * Time.deltaTime)
        {
            timeUntilEndTurn -= Time.deltaTime;
            if (timeUntilEndTurn < 0)
            {
                Debug.LogWarning(name + "Pass turn");
                StartCoroutine(endAI());
            }

        }

        if (target != null && target.TryGetComponent<UnitScript>(out UnitScript u) && u.isDead())
        {
            stopMoveToTarget();
            activateUnit = false;
            target = null;
        }

        if (!canMove())
        {

            stopMoveToTarget();
        }

        //Segment attacks
        if (checkTargetInAttackRange() && canAction())
        {
            if (isMoving)
            {
                stopMoveToTarget();
            }
            weaponAttack(targetList[0].transform.position);
            if (canAction())
            {
                StartCoroutine(pause());
            }
        }

        //Main behavior
        if (checkTargetInAttackRange() && !canAction() && canMove())
        {
            stopMoveToTarget();
            StartCoroutine(endAI());
        }

        else if (isMoving && canMove())
        {
            moveUnit(transform.position - locationLastFrame);
            if (checkTargetInAttackRange() && !canAction())
            {
                stopMoveToTarget();
                StartCoroutine(endAI());
            }
        }
        else if (!canMove() && canAction())
        {
            print(name + "AI Deciding");
            pickDashOrAbility();

        }
        else if (canMove())
        {
            if (!activateUnit && findClosestTarget() && checkingTargetInDetectRange())
            {
                activateUnit = true;
                startMoveToTarget();
            }
        }
        else
        {
            stopMoveToTarget();
            StartCoroutine(endAI());

        }
        locationLastFrame = transform.position;
    }


    bool checkTargetInAttackRange()
    {
        //print(name + " checkingTargetInAttackRange");
        targetList = new List<GameObject>();
        RaycastHit2D[] tempGO = Physics2D.CircleCastAll(transform.position, attackRange, new Vector2(), 0, base.layerMask_target);
        foreach (RaycastHit2D r in tempGO)
        {
            //print(tempGO.Length);
            if (r.collider.TryGetComponent<UnitScript>(out UnitScript w))
            {
                //print(r.collider.name);

                if (!w.isDead() && Physics2D.Raycast(r.transform.position, transform.position - r.transform.position, attackRange, layerMask_insight).collider == null)
                {
                    //print(r.collider.name + " added to list");
                    targetList.Add(r.collider.gameObject);

                }
            }
        }
        return targetList.Capacity > 0;
    }


    bool findClosestTarget()
    {
        print(name + " findClosestTarget");

        targetList = new List<GameObject>();
        RaycastHit2D[] tempGO = Physics2D.CircleCastAll(transform.position, detectionRange * 10f, new Vector2(), 0, base.layerMask_target);
        foreach (RaycastHit2D r in tempGO)
        {
            if (r.collider.TryGetComponent<UnitScript>(out UnitScript w) && !w.isDead())
            {
                targetList.Add(r.collider.gameObject);

            }
        }
        return targetList.Capacity > 0;
    }

    bool checkingTargetInDetectRange()
    {
        print(name + " checkingTargetInDetectRange");
        target = targetList[0];
        float dis = (target.transform.position - transform.position).magnitude;
        float compareDis = 0;
        foreach (GameObject g in targetList)
        {
            compareDis = (g.transform.position - transform.position).magnitude;
            if (compareDis < dis)
            {
                target = g;
                dis = compareDis;
            }
        }
        return dis <= detectionRange;
    }

    void startMoveToTarget()
    {
        print(name + " startMoveToTarget");
        if (target.GetComponent<UnitScript>().isDead())
        {
            stopMoveToTarget();
            activateUnit = false;
        }

        aIDestinationSetter.target = target.transform;
        aIPath.SearchPath();
        isMoving = true;
        aIPath.canSearch = true;
        aIPath.canMove = true;
        return;
    }

    void resumeMoveToTarget()
    {
        //resume chasing the last target
        print(name + " resumeMoveToTarget");
        aIDestinationSetter.target = target.transform;
        aIPath.SearchPath();
        isMoving = true;
        aIPath.canSearch = true;
        aIPath.canMove = true;
        return;
    }

    void stopMoveToTarget()
    {
        print(name + " stopMoveToTarget");

        aIDestinationSetter.target = transform;
        aIPath.SearchPath();
        isMoving = false;
        aIPath.canSearch = false;
        aIPath.canMove = false;
    }

    public override void moveUnit(Vector2 dir)
    {
        //print(name + " MoveUnit");

        if (canMove())
        {
            moveDirection = dir;
            animator.SetFloat("Speed", moveDirection.magnitude * 100f);
            animator.SetFloat("H_Speed", moveDirection.x * 100f);
            movement_current -= dir.magnitude;

        }
        else
        {
            stopMove();
            stopMoveToTarget();
        }

    }


    public void weaponAttack(Vector2 pos)
    {
        print(name + " weaponAttack");
        isMoving = false;
        targetPosition = pos;
        GameObject tempWeapon = Instantiate(mainWeapon, transform.position, transform.rotation);
        tempWeapon.GetComponent<WeaponScript>().attack(targetPosition, toHit, baseDamage, weaponDamage);
        //weaponList.Add(tempWeapon);
        animator.SetTrigger("Attack");

        disableAimObject_Attack();
        actionCount--;
    }

    public override void endTurn()
    {
        base.endTurn();
        StopCoroutine(pause());
        StopCoroutine(endAI());
    }

    public override void die()
    {
        randomLootSpawnScript.showerLoot();
        base.die();
    }


    public void pickDashOrAbility()
    {
        if (abilityClassScript == null)
        {
            print(name + "cant find ability");

            dash();
            startMoveToTarget();

            return;
        }

        int choice = Random.Range(0, 10);
        if (choice > 6)
        {
            print(name + "use ability");

            useAbility();
        }
        else
        {
            print(name + "Dashing");

            dash();
            startMoveToTarget();
        }
    }
    public override void useAbility()
    {
        if (ammo_Current > 0)
        {
            int amount = abilityClassScript.useAbility(abilityRange, layerMask_insight, abilityLevel);
            if (amount != 0)
            {
                ammo_Current--;
                PlaySound_Ability();
                actionCount = 0;

            }
        }
        base.useAbility();
        StartCoroutine(endAI());

    }
}
