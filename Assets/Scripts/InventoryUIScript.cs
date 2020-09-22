using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryUIScript : MonoBehaviour
{
    public PlayerManagerScript playerManagerScript;
    public TextMeshProUGUI goldText;
    public List<Image> lootDisplayImages;
    public Sprite emptySprite;

    private void Awake()
    {
        playerManagerScript = GameObject.FindObjectOfType<PlayerManagerScript>();
        playerManagerScript.setInventoryUI(this);
        updateLootDisplay(playerManagerScript.money, playerManagerScript.loot);
    }
    private void Start()
    {
        updateLootDisplay(playerManagerScript.money, playerManagerScript.loot);

    }

    private void FixedUpdate()
    {
    }

    private void OnEnable()
    {
        updateLootDisplay(playerManagerScript.money, playerManagerScript.loot);

    }

    public void updateLootDisplay(int amount, List<GameObject> loot)
    {
        goldText.text = "G:" + amount;

        int lootAmount = loot.Count;
        for (int i = 0; i < lootDisplayImages.Count; i++)
        {
            if (i < lootAmount)
            {
                Sprite image = loot[i].GetComponentInChildren<SpriteRenderer>().sprite;
                lootDisplayImages[i].sprite = image;
            }
            else
            {
                lootDisplayImages[i].sprite = emptySprite;

            }
        }
    }
}
