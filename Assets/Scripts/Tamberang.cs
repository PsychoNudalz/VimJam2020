using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tamberang : AbilityClassScript
{

    // Start is called before the first frame update
    public override void getEffectedList(float abilityRange, LayerMask mask)
    {
        effectedList = new List<GameObject>();
        //base.getEffectedList(abilityRange);
        RaycastHit2D[] tempGO = Physics2D.CircleCastAll(transform.position, abilityRange, new Vector2(), 0, layerMask);
        foreach (RaycastHit2D r in tempGO)
        {
            if (r.collider.TryGetComponent<WeaponScript>(out WeaponScript w))
            {

                if (!Physics2D.Raycast(r.transform.position, transform.position - r.transform.position, (transform.position - r.transform.position).magnitude, mask))
                {
                    effectedList.Add(r.collider.gameObject);

                }
            }
        }
    }
    public override int useAbility(float abilityRange, LayerMask mask, int amount = 0)
    {
        getEffectedList(abilityRange,mask);
        foreach (GameObject g in effectedList)
        {
            if (g.TryGetComponent<WeaponScript>(out WeaponScript w))
            {
                w.recallWeapon(transform.position);
            }

        }
        return effectedList.Count;
    }

    public override void displayEffectedList(float abilityRange, LayerMask mask)
    {
        if (effectedList.Count > 0)
        {
            foreach (GameObject g in effectedList)
            {
                try
                {
                    if (g.TryGetComponent<WeaponScript>(out WeaponScript w))
                    {
                        w.highLight(false);
                    }
                } catch(System.Exception e)
                {
                    //Debug.LogWarning(e);
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
                    if (g.TryGetComponent<WeaponScript>(out WeaponScript w))
                    {
                        w.highLight(true);
                    }
                }
                catch (System.Exception e)
                {
                    //Debug.LogWarning(e);

                }
            }
        }
    }
}
