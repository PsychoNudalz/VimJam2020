using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootPickupScript : MonoBehaviour
{
    [SerializeField] PlayerManagerScript playerManagerScript;
    public DamagePopUpManagerScript damagePopUpManagerScript;
    public LootType lootType;
    public int lootValue;
    public float lootRarity = 1;

    [Header("Animator")]
    public Animator animator;
    public string animationName = "LootItem_NA";


    [Header("Movement")]
    [SerializeField] bool isMoving = false;
    public Vector2 moveLocation;

    private void Awake()
    {
        playerManagerScript = GameObject.FindObjectOfType<PlayerManagerScript>();
        damagePopUpManagerScript = GameObject.FindObjectOfType<DamagePopUpManagerScript>();
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
        animator.Play(animationName);

    }

    private void FixedUpdate()
    {
        if (isMoving)
        {

            moveToLocation();
        }
    }
    private void OnEnable()
    {
        animator.Play(animationName);

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pickUpLoot();
        }
    }


    public void pickUpLoot()
    {
        switch (lootType)
        {
            case (LootType.MONEY):
                playerManagerScript.addMoney(lootValue);
                displayLoot(lootValue.ToString());
                Destroy(gameObject);
                break;
            case (LootType.ITEM):
                playerManagerScript.addLoot(gameObject);
                displayLoot("LOOOOT!");
                gameObject.SetActive(false);
                break;
        }
    }

    void displayLoot(string text)
    {
        float randomPos = 0.2f;
        damagePopUpManagerScript.newDamageText(text, transform.position + new Vector3(Random.Range(-randomPos, randomPos), Random.Range(-randomPos, randomPos)), Color.yellow);

    }

    public void moveToLocation(Vector2 pos)
    {
        moveLocation = pos;
        isMoving = true;
        transform.position = Vector3.Lerp(transform.position, moveLocation, 2f * Time.deltaTime);
    }

    void moveToLocation()
    {
        transform.position = Vector3.Lerp(transform.position, moveLocation, 2f * Time.deltaTime);

        if ((transform.position - (Vector3)moveLocation).magnitude < 0.2f)
        {
            isMoving = false;
        }
    }
}
