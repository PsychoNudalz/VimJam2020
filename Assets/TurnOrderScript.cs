using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOrderScript : MonoBehaviour
{
    public List<TurnOrderPieceScript> turnOrderPieceScripts;
    public Animator animator;
    [SerializeField] List<UnitScript> turnOrder;

    public void updateTurnOrder(List<UnitScript> u)
    {
        turnOrder = u;
        StartCoroutine(PlayAnimationBeforeUpdate());
    }

    IEnumerator PlayAnimationBeforeUpdate()
    {
        print("Update Turn Order");
        animator.Play("TurnOrder_Shift");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length);
        for (int i = 0; i < turnOrderPieceScripts.Count && i < turnOrder.Count; i++)
        {
            turnOrderPieceScripts[i].setTurn(turnOrder[i].spriteRenderer.sprite, turnOrder[i].unitName + " Lv." + turnOrder[i].level);
        }
        animator.Play("TurnOrder_Idle");


    }
}
