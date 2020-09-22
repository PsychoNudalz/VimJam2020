using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandlerScript : MonoBehaviour
{
    public BattleUIScript battleUIScript;
    public StatesMenuUIScript statesMenuUIScript;
    public InventoryUIScript inventoryUIScript;

    public void disableAllMenu()
    {
        battleUIScript.gameObject.SetActive(false);
        statesMenuUIScript.gameObject.SetActive(false);
        inventoryUIScript.gameObject.SetActive(false);
        statesMenuUIScript.upgrade_SetActive(false);
    }

    public void battleMode()
    {
        disableAllMenu();
        battleUIScript.gameObject.SetActive(true);
        inventoryUIScript.gameObject.SetActive(true);

    }

    public void upradeMode()
    {
        disableAllMenu();
        statesMenuUIScript.gameObject.SetActive(true);
        inventoryUIScript.gameObject.SetActive(true);
        statesMenuUIScript.upgrade_SetActive(true);

    }

    public void statsMode()
    {
        disableAllMenu();
        statesMenuUIScript.gameObject.SetActive(true);
        inventoryUIScript.gameObject.SetActive(true);
    }

    public void toggle_upgradeMode()
    {
        if (statesMenuUIScript.gameObject.activeSelf)
        {
            battleMode();
        }
        else
        {
            upradeMode();
        }
    }
}
