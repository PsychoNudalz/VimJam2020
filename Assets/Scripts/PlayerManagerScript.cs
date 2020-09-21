using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagerScript : MonoBehaviour
{
    public int money = 0;
    public List<GameObject> loot;
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
        return money;
    }

    public List<GameObject> addLoot(GameObject newLoot)
    {
        loot.Add(newLoot);
        return loot;
    }

    public bool removeMoney(int value)
    {
        if (money < value)
        {
            money -= value;
            return true;

        }

        return false;
    }


    //Activating Units
    public void activateUnits()
    {
        foreach(UnitScript u in units)
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
        money = data.money;
    }

}
