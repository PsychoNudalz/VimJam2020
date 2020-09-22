using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScript_Pyp : UnitScript
{
    public override void weaponAttack()
    {
        if (ammo_Current > 0)
        {

            base.weaponAttack();
            ammo_Current--;

        }
    }
    public override void useAbility()
    {
        base.useAbility();
        int amount = abilityClassScript.useAbility(abilityRange, layerMask_insight);
        if (amount != 0)
        {
            ammo_Current += amount;
            actionCount--;
        }

    }

    public override string ToString_Ability()
    {
        return ammo.ToString();
    }

    public override int Upgrade_Ability()
    {
        ammo += upgrade_Ability;
        ammo_Current++;
        abilityLevel++;
        level++;
        return ammo;
    }
}
