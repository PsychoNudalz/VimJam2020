using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScript_Knight : UnitScript
{
    public override void useAbility()
    {
        base.useAbility();
        if (ammo_Current > 0)
        {
            int amount = abilityClassScript.useAbility(abilityRange, layerMask_insight, abilityLevel);
            if (amount != 0)
            {
                ammo_Current--;
                actionCount--;
                PlaySound_Ability();

            }
        }

    }

    public override string ToString_Ability()
    {
        return abilityLevel.ToString();
    }

    public override int Upgrade_Ability()
    {
        abilityLevel += upgrade_Ability;
        ammo ++;
        level++;
        return ammo;
    }
}
