using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// References the gameObject's position and scale and pick a random 2D co-ordanates
/// </summary>
public class SpawnAreaScript : MonoBehaviour
{
    [SerializeField] Vector2 worldSpawnPoint;

    private void Start()
    {
        //print(getRandomPos());
    }

    public virtual Vector2 getRandomPos()
    {
        worldSpawnPoint = new Vector2(Random.Range(-transform.lossyScale.x/2f, transform.lossyScale.x / 2f) +transform.position.x, Random.Range(-transform.lossyScale.y / 2f, transform.lossyScale.y / 2f) + transform.position.y);
        return worldSpawnPoint;

    }
}
