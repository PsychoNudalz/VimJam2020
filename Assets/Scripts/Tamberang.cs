using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tamberang : AbilityClassScript
{

    // Start is called before the first frame update
    public override void getEffectedList(float abilityRange)
    {
        effectedList = new List<GameObject>();
        base.getEffectedList(abilityRange);
        RaycastHit2D[] tempGO = Physics2D.CircleCastAll(transform.position, abilityRange, new Vector2(), 0, layerMask);
        foreach (RaycastHit2D r in tempGO)
        {
            print(tempGO.Length);
            if (r.collider.TryGetComponent<WeaponScript>(out WeaponScript w))
            {
                print(r.collider.name);
                effectedList.Add(r.collider.gameObject);

                /*
                if (Physics2D.Raycast(r.transform.position, transform.position - r.transform.position, abilityRange, layerMask).collider.gameObject.Equals(gameObject))
                {
                    print(r.collider.name+" added to list");

                }
                */
            }
        }
    }
    public override int useAbility(float abilityRange)
    {
        print(name + " use recall ability");
        getEffectedList(abilityRange);
        foreach (GameObject g in effectedList)
        {
            if (g.TryGetComponent<WeaponScript>(out WeaponScript w))
            {
                w.recallWeapon(transform.position);
            }

        }
        return effectedList.Capacity;

    }
}
