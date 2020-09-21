using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePopUpManagerScript : MonoBehaviour
{
    [SerializeField] GameObject damagePopUpPF;
    [SerializeField] List<DamagePopUpScript> pool;
    public int poolSize = 10;
    [SerializeField] int poolPointer = -1;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            pool.Add(Instantiate(damagePopUpPF, transform).GetComponent<DamagePopUpScript>());
        }
    }

    public void newDamageText(string s, Vector2 pos, Color colour)
    {
        DamagePopUpScript currentPopUp = getNextPopUp();
        currentPopUp.setText(s, colour);
        currentPopUp.transform.position = pos;
        currentPopUp.gameObject.SetActive(true);
    }

    DamagePopUpScript getNextPopUp()
    {
        poolPointer++;
        poolPointer = poolPointer % poolSize;
        return pool[poolPointer];
    }

}
