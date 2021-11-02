using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Enemy[] enemyTypes;
    [SerializeField] float spawnDelay = 1f;

    //[SerializeField] bool 

    float timeAfterLastSpawn = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() 
    {
        timeAfterLastSpawn += Time.deltaTime;
        if(timeAfterLastSpawn >= spawnDelay)
        {
            Enemy newEnemy = Instantiate(enemyTypes[Random.Range(0, enemyTypes.Length-1)], transform.position, Quaternion.identity) as Enemy;
            timeAfterLastSpawn = 0;
        }
    }
}
