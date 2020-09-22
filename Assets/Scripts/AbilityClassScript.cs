using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityClassScript : MonoBehaviour
{

    public LayerMask layerMask;
    public List<string> targetTag;
    public List<GameObject> effectedList;

    private void Update()
    {

    }

    public virtual void getEffectedList(float abilityRange, LayerMask mask)
    {
        
    }

    public virtual int useAbility(float abilityRange, LayerMask mask, int amount = 0)
    {
        return effectedList.Capacity;
    }

    public virtual void displayEffectedList(float abilityRange, LayerMask mask)
    {

    }

    internal void showEffectedList(float abilityRange, LayerMask layerMask_insight)
    {
        throw new NotImplementedException();
    }
}
