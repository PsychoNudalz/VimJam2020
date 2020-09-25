using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleUIScript : MonoBehaviour
{
    [Header("Components")]
    public TurnOrderScript turnOrder;
    public TextMeshProUGUI objectiveText;
    [Header("Compass")]
    public LootCompassScript lootCompassScript;

    public void updateTurnOrder(List<UnitScript> u)
    {
        turnOrder.updateTurnOrder(u);
    }

    public void updateObjectiveText((string, Color) x)
    {
        objectiveText.text = x.Item1;
        objectiveText.color = x.Item2;
    }

    public void updateCompass(Vector2 currentTurn,List<GameObject> loot)
    {
        lootCompassScript.updateCompass(currentTurn, loot);
    }
}
