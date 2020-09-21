using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [Header("Stats")]
    public int damageDice = 4;
    public float moveSpeed = 5f;

    [Header("Rolled Values")]
    public int toHit;
    public int damageBonus;

    public List<string> targetTag;
    [SerializeField] List<GameObject> hitObjects;

    [Header("Target")]
    public Vector2 targetPosition;

    [Header("Extra")]
    public bool isRecall;


    private void Update()
    {
        moveToLocation();
        if (isRecall && (targetPosition - (Vector2)transform.position).magnitude < 0.3f)
        {
            Destroy(gameObject);
        }
    }


    void moveToLocation()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed*Time.deltaTime);
    }

    public void recallWeapon(Vector2 pos)
    {
        targetPosition = pos;
        isRecall = true;
        hitObjects = new List<GameObject>();
    }

    public void attack(Vector2 pos,int t, int d)
    {
        targetPosition = pos;
        toHit = t;
        damageBonus = d;
    }

    void damageBehaviour(Collider2D collision)
    {
        //print(name + " hit " + collision);
        if (targetTag.Contains(collision.tag) && !hitObjects.Contains(collision.gameObject))
        {
            if (collision.tag.Equals("Enemy"))
            {
                int damage = Mathf.FloorToInt(Random.Range(1, damageDice)) + damageBonus;
                collision.GetComponent<UnitScript>().takeDamage(damage,Mathf.FloorToInt(Random.RandomRange(1,21)+toHit));
                hitObjects.Add(collision.gameObject);
            }
            if (collision.tag.Equals("Player"))
            {
                int damage = Mathf.FloorToInt(Random.Range(1, damageDice)) + damageBonus;
                collision.GetComponent<UnitScript>().takeDamage(damage, Mathf.FloorToInt(Random.RandomRange(1, 21) + toHit));
                hitObjects.Add(collision.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        damageBehaviour(collision);
    }

    private void OnTriggerExit2D (Collider2D collision)
    {
        damageBehaviour(collision);
    }
}
