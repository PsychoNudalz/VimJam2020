using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleUIScript : MonoBehaviour
{
    public TurnOrderScript turnOrder;
    public TextMeshProUGUI objectiveText;

    public void updateTurnOrder(List<UnitScript> u)
    {
        turnOrder.updateTurnOrder(u);
    }

    public void updateObjectiveText((string, Color) x)
    {
        objectiveText.text = x.Item1;
        objectiveText.color = x.Item2;
    }

}
