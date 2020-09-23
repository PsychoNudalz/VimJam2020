using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAbility : AbilityClassScript
{
    public override void getEffectedList(float abilityRange, LayerMask mask)
    {
        effectedList = new List<GameObject>();
        //base.getEffectedList(abilityRange);
        RaycastHit2D[] tempGO = Physics2D.CircleCastAll(transform.position, abilityRange, new Vector2(), 0, layerMask);
        foreach (RaycastHit2D r in tempGO)
        {
            if (r.collider.TryGetComponent<UnitScript>(out UnitScript w))
            {
                if (r.collider.CompareTag("Player")&& !Physics2D.Raycast(r.transform.position, transform.position - r.transform.position, (transform.position - r.transform.position).magnitude, mask))
                {
                    effectedList.Add(r.collider.gameObject);
                }
            }
        }
    }
    public override int useAbility(float abilityRange, LayerMask mask, int amount = 0)
    {
        getEffectedList(abilityRange, mask);
        foreach (GameObject g in effectedList)
        {
            if (g.TryGetComponent<UnitScript>(out UnitScript w))
            {
                w.healUnit(Mathf.RoundToInt(Random.Range(1, amount)) + Mathf.RoundToInt(amount/2f));
            }

        }
        return effectedList.Capacity;
    }

    public override void displayEffectedList(float abilityRange, LayerMask mask)
    {
        if (effectedList.Count > 0)
        {
            foreach (GameObject g in effectedList)
            {
                try
                {
                    if (g.TryGetComponent<UnitScript>(out UnitScript w))
                    {
                        w.OutlineSelf_Off();
                    }
                }
                catch (System.Exception e)
                {
                    Debug.LogWarning(e);
                }
            }
        }
        getEffectedList(abilityRange, mask);
        if (effectedList.Count > 0)
        {
            foreach (GameObject g in effectedList)
            {
                try
                {
                    if (g.TryGetComponent<UnitScript>(out UnitScript w))
                    {
                        w.OutlineSelf_On(Color.green);

                    }
                }
                catch (System.Exception e)
                {
                    Debug.LogWarning(e);

                }
            }
        }
    }
}
