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

    [Header("Current Quest")]
    public QuestType questType;

    public static PlayerManagerScript instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            questType = instance.questType;
            Destroy(instance.gameObject);
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

    public (int, string) getLoot(int i)
    {
        if (i < loot.Count)
        {
            LootPickupScript tempLoot = loot[i].GetComponent<LootPickupScript>();
            return (tempLoot.lootValue, tempLoot.lootID);
        }
        else
        {
            return (0, null);
        }
    }

    public void sellLoot(int i)
    {
        if (getLoot(i).Item2 != null)
        {
            LootPickupScript tempLoot = loot[i].GetComponent<LootPickupScript>();

            removeLoot(-1, tempLoot.lootID);
            addMoney(tempLoot.lootValue);
        }
    }

    public void sellAllLoot()
    {
        for (int i = 0; i < loot.Count; i++)
        {
            sellLoot(i);
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
    public float getAverageLevel()
    {
        int tempI = 0;
        foreach(UnitScript u in units)
        {
            tempI += u.level;
        }
        return tempI / units.Count;
    }

    public float getTotalLevel()
    {
        int tempI = 0;
        foreach (UnitScript u in units)
        {
            tempI += u.level;
        }
        return tempI;
    }



    //Activating Units
    public void activateUnits()
    {
        float i = -units.Count / 2f;
        foreach (UnitScript u in units)
        {
            
            u.gameObject.SetActive(true);
            u.resetCurrentStats();
            u.transform.position = new Vector2(i, 0);
            i++;
            //FindObjectOfType<BattleSystem>().addTurn(u);
        }

    }

    public void deactiveUnits()
    {
        SavePlayer();
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

   

    //Displaying stats

    public override string ToString()
    {
        string returnS =
            "Current Party \n" +
            "Avg. Level: "+(Mathf.RoundToInt(getAverageLevel()))+"\n" +
            "Party Gold: "+money.ToString();
        return returnS;
    }

    //Check game over
    public bool isPlayersDead()
    {
        foreach(UnitScript u in units)
        {
            if (!u.isDead())
            {
                return false;
            }
        }
        return true;
    }

}
