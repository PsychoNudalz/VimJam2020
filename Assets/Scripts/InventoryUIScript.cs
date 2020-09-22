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

    private void Awake()
    {
        playerManagerScript = GameObject.FindObjectOfType<PlayerManagerScript>();
        playerManagerScript.setInventoryUI(this);
        
    }

    private void FixedUpdate()
    {
    }


    public void updateLootDisplay(int amount, List<GameObject> loot)
    {
        goldText.text = "G:" + amount;

        int lootAmount = loot.Count;
        for (int i = 0; i < lootAmount && i <lootDisplayImages.Count; i++)
        {
            Sprite image = loot[i].GetComponentInChildren<SpriteRenderer>().sprite;
            lootDisplayImages[i].sprite = image;
        }
    }
}
