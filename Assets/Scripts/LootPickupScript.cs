using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootPickupScript : MonoBehaviour
{
    [SerializeField] PlayerManagerScript playerManagerScript;
    public LootType lootType;
    public int lootValue;
    public float lootRarity = 1;

    [Header("movement")]
    [SerializeField] bool isMoving = false;
    public Vector2 moveLocation;

    private void Awake()
    {
        playerManagerScript = GameObject.FindObjectOfType<PlayerManagerScript>();
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {

            moveToLocation();
        }
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
                Destroy(gameObject);
                break;
            case (LootType.ITEM):
                playerManagerScript.addLoot(gameObject);
                gameObject.SetActive(false);
                break;
        }
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
