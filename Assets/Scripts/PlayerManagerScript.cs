using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagerScript : MonoBehaviour
{
    [Header("Components")]
    public InventoryUIScript inventoryUIScript;

    [Header("Loot")]
    public int money = 0;
    public List<GameObject> loot;

    [Header("Units")]
    public List<UnitScript> units;

    public static PlayerManagerScript instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
        //inventoryUIScript = GameObject.FindObjectOfType<InventoryUIScript>();

        DontDestroyOnLoad(gameObject);
        LoadPlayer();
    }

    private void OnApplicationQuit()
    {
        SavePlayer();
    }


    //Modifying Loot
    public int addMoney(int value)
    {
        money += value;
        updateLootDisplay();
        return money;
    }

    public List<GameObject> addLoot(GameObject newLoot)
    {
        loot.Add(newLoot);
        updateLootDisplay();
        newLoot.transform.SetParent(transform);
        return loot;
    }

    public bool removeMoney(int value)
    {
        if (money < value)
        {
            money -= value;
            updateLootDisplay();
            return true;

        }

        return false;
    }

    public int checkHasLoot(string id)
    {
        for (int i = 0; i < loot.Count; i++)
        {
            LootPickupScript tempLoot = loot[i].GetComponent<LootPickupScript>();
            if (tempLoot.lootID.Contains(id) || id.Contains(tempLoot.lootID))
            {
                return i;
            }
        }
        return -1;
    }

    public bool removeLoot(int c, string id)
    {
        int indexL = checkHasLoot(id);
        if (indexL != -1 && money >= c)
        {
            loot.RemoveAt(indexL);
            money -= c;
            return true;
        }
        else
        {
            return false;
        }
    }

    //Update UI

    public void setInventoryUI(InventoryUIScript i)
    {
        inventoryUIScript = i;
    }

    public void updateLootDisplay()
    {
        inventoryUIScript.updateLootDisplay(money, loot);
    }



    //Activating Units
    public void activateUnits()
    {
        foreach (UnitScript u in units)
        {
            u.gameObject.SetActive(true);
        }
    }

    public void deactiveUnits()
    {
        foreach (UnitScript u in units)
        {
            u.gameObject.SetActive(false);
        }
    }



    //Save Load
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if (data == null)
        {
            money = 0;
            foreach(UnitScript u in units)
            {
                u.RESET_UNIT();
            }
        }
        else
        {
            money = data.money;
            for (int i = 0; i < units.Count; i++)
            {
                units[i].saveDataToStates(data.unitStates[i]);
            }
        }
    }

}
