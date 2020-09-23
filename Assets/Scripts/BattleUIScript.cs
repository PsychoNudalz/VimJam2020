using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUIScript : MonoBehaviour
{
    public TurnOrderScript turnOrder;

    public void updateTurnOrder(List<UnitScript> u)
    {
        turnOrder.updateTurnOrder(u);
    }

}
