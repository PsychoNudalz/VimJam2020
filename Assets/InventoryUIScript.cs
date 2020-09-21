using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class InventoryUIScript : MonoBehaviour
{
    public PlayerManagerScript playerManagerScript;
    public TextMeshProUGUI goldText;

    private void Awake()
    {
        playerManagerScript = GameObject.FindObjectOfType<PlayerManagerScript>();
    }

    private void FixedUpdate()
    {
        goldText.text = "G:" + playerManagerScript.money;
    }
}
