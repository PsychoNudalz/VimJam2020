using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class StatesMenuUIScript : MonoBehaviour
{
    [Header("Components")]
    public PlayerManagerScript playerManagerScript;
    public InventoryUIScript inventoryUIScript;

    [Header("Unit Display")]
    public UnitStatesDisplayScript unitStatesDisplayScript;

    [Header("Picker Button")]
    public List<Image> buttonImages;

    [Header("Upgrade")]
    public bool isUpgradeMode;
    public GameObject upgradMenu;

    [Header("Sell")]
    //public List<Button> sellButtons;
    public List<TextMeshProUGUI> sellButtonTexts;


    private void OnEnable()
    {
        if (playerManagerScript == null)
        {
            playerManagerScript = GameObject.FindObjectOfType<PlayerManagerScript>();
        }

        if (inventoryUIScript == null)
        {
            inventoryUIScript = GameObject.FindObjectOfType<InventoryUIScript>();
        }
        unitStatesDisplayScript.updateStates(playerManagerScript.units[0]);
    }


    private void FixedUpdate()
    {
        if (gameObject.activeSelf)
        {
            updateButtons();
            updateSellCost();
        }
    }

    public void displayUnit(int i)
    {
        unitStatesDisplayScript.updateStates(playerManagerScript.units[i]);
    }

    public void updateButtons()
    {
        buttonImages[0].sprite = playerManagerScript.units[0].GetComponentInChildren<SpriteRenderer>().sprite;
        buttonImages[1].sprite = playerManagerScript.units[1].GetComponentInChildren<SpriteRenderer>().sprite;
        buttonImages[2].sprite = playerManagerScript.units[2].GetComponentInChildren<SpriteRenderer>().sprite;

    }

    public void upgrade_SetActive(bool s)
    {
        isUpgradeMode = s;
        upgradMenu.SetActive(s);
    }

    public bool tryUpgrade(StatsEnum s, int c, string id)
    {
        inventoryUIScript.updateLootDisplay(playerManagerScript.money, playerManagerScript.loot);
        unitStatesDisplayScript.updateStates();
        if (playerManagerScript.removeLoot(c, id))
        {
            UnitScript currentUnit = unitStatesDisplayScript.currentUnit;
            switch (s)
            {
                case (StatsEnum.HP):
                    currentUnit.Upgrade_HP();
                    break;
                case (StatsEnum.AC):
                    currentUnit.Upgrade_AC();
                    break;
                case (StatsEnum.SPEED):
                    currentUnit.Upgrade_Movement();
                    break;
                case (StatsEnum.TOHIT):
                    currentUnit.Upgrade_AttackBonus();
                    break;
                case (StatsEnum.WEAPONDAMAGE):
                    currentUnit.Upgrade_WeaponDamage();
                    break;
                case (StatsEnum.ABILITY):
                    currentUnit.Upgrade_Ability();
                    break;
            }

            inventoryUIScript.updateLootDisplay(playerManagerScript.money, playerManagerScript.loot);
            unitStatesDisplayScript.updateStates();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void resetUnitStats()
    {
        unitStatesDisplayScript.currentUnit.RESET_UNIT();
        unitStatesDisplayScript.updateStates();
    }




    public void sellItem(int i)
    {
        playerManagerScript.sellLoot(i);
    }

    public void updateSellCost()
    {
        (int, string) returnValues;
        for (int i = 0; i < sellButtonTexts.Count; i++)
        {
            returnValues = playerManagerScript.getLoot(i);
            print(returnValues.Item2);
            sellButtonTexts[i].text = "G:"+returnValues.Item1.ToString();
        }
    }
}
