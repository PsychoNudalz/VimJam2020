using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLootSpawnScript : MonoBehaviour
{
    public int lootValue;
    public float spawnZone = 1;
    [SerializeField] List<GameObject> potentialLoot;
    public List<GameObject> deathLoot;

    private void Awake()
    {
        generateLoot();
    }


    public void generateLoot()
    {
        GameObject randomLoot;

        while (lootValue > 0)
        {
            randomLoot = potentialLoot[Mathf.FloorToInt(Random.Range(0, potentialLoot.Count-0.001f))];

            if (lootValue - randomLoot.GetComponent<LootPickupScript>().lootValue >= 0)
            {
                lootValue -= randomLoot.GetComponent<LootPickupScript>().lootValue;
                randomLoot = Instantiate(randomLoot, transform.position, Quaternion.identity, transform);
                deathLoot.Add(randomLoot.gameObject);

            }
            if (lootValue == 1)
            {
                randomLoot = potentialLoot[0];
                randomLoot = Instantiate(randomLoot, transform.position, Quaternion.identity, transform);
                deathLoot.Add(randomLoot.gameObject);
            }
        }

        foreach (GameObject g in deathLoot)
        {
            g.SetActive(false);
        }
    }

    public void showerLoot()
    {

        foreach (GameObject g in deathLoot)
        {
            g.transform.position = transform.position;
            g.SetActive(true);
            g.GetComponent<LootPickupScript>().moveToLocation(transform.position + new Vector3(Random.Range(-spawnZone, spawnZone), Random.Range(-spawnZone, spawnZone)));
            g.transform.parent = null;
        }
    }
}
