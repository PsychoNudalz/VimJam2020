using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootCompassScript : MonoBehaviour
{
    [Header("Compass")]
    public GameObject closestLoot;
    //public List<GameObject> loot;
    public Animator compassAnimator;
    public Image lootTargetSprite;
    public Vector2 lootDir;


    public void updateCompass(Vector3 pos, List<GameObject> loot)
    {

        if (loot.Count < 1)
        {
            lootTargetSprite.gameObject.SetActive(false);
            compassAnimator.SetFloat("x", 0);
            compassAnimator.SetFloat("y", 0);
            return;
        }

        lootTargetSprite.gameObject.SetActive(true);

        closestLoot = loot[0];
        float dis = (closestLoot.transform.position - pos).magnitude;

        foreach (GameObject l in loot)
        {
            if((l.transform.position - pos).magnitude < dis)
            {
                dis = (l.transform.position - pos).magnitude;
                closestLoot = l;
            }
        }

        lootDir = (closestLoot.transform.position - pos).normalized;
        compassAnimator.SetFloat("x", lootDir.x);
        compassAnimator.SetFloat("y", lootDir.y);
        lootTargetSprite.sprite = closestLoot.GetComponentInChildren<SpriteRenderer>().sprite;
    }

    void playAnimation()
    {
        float x = lootDir.x;
        float y = lootDir.y;
    }
}
