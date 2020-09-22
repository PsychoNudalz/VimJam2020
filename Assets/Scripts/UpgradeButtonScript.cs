using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeButtonScript : MonoBehaviour
{
    [Header("Components")]
    public StatesMenuUIScript statesMenuUIScript;
    public TextMeshProUGUI goldText;

    [Header("Upgrade")]
    public int cost = 100;
    public string itemID;
    public StatsEnum statsEnum;

    private void Awake()
    {
        if (statesMenuUIScript != null)
        {
            statesMenuUIScript = GetComponentInParent<StatesMenuUIScript>();

        }
        goldText.color = Color.yellow;

    }

    private void OnEnable()
    {
        goldText.color = Color.yellow;
        if (statesMenuUIScript != null)
        {
            statesMenuUIScript = GetComponentInParent<StatesMenuUIScript>();

        }
    }

    public void useButton()
    {
        if (statesMenuUIScript.tryUpgrade(statsEnum, cost, itemID))
        {
            goldText.color = Color.green;

        }
        else
        {
            goldText.color = Color.red;
        }
    }

}
