using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootPickupScript : MonoBehaviour
{
    [SerializeField] PlayerManagerScript playerManagerScript;
    public LootType lootType;
    public int lootValue;
    public float lootRarity = 1;

    private void Awake()
    {
        playerManagerScript = GameObject.FindObjectOfType<PlayerManagerScript>();
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
}
