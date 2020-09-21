﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityClassScript : MonoBehaviour
{

    public LayerMask layerMask;
    public List<string> targetTag;
    public List<GameObject> effectedList;
    
    public virtual void getEffectedList(float abilityRange)
    {
        
    }

    public virtual int useAbility(float abilityRange)
    {
        print("used base");
        return effectedList.Capacity;
    }

}
