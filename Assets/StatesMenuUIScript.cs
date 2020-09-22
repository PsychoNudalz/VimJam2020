using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatesMenuUIScript : MonoBehaviour
{
    [Header("Components")]
    public PlayerManagerScript playerManagerScript;

    [Header("Unit Display")]
    public UnitStatesDisplayScript unitStatesDisplayScript;

    [Header("Picker Button")]
    public List<Image> buttonImages; 

    private void OnEnable()
    {
        if (playerManagerScript == null)
        {
            playerManagerScript = GameObject.FindObjectOfType<PlayerManagerScript>();
        }
    }


    private void FixedUpdate()
    {
        updateButtons();
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

}
