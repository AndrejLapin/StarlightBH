using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] float agroRange = 35f;
    [SerializeField] float avoidDistance = 13f;
    [SerializeField] Weapon weaponPrefab;

    [SerializeField] float rotationAngle = 90f; // no clue how it works
    Weapon weaponInstance;
    Player[] players;
    NavMeshAgent navMeshAgent;
    int targetPlayerIndex = 0;
    float distanceToTarget = Mathf.Infinity;

    void OnDrawGizmosSelected()
    {
        // Display the agro range of the enemy
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, agroRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, avoidDistance);
    }

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        players = FindObjectsOfType<Player>();
        if(weaponPrefab)
        {
            weaponInstance = (Weapon)weaponPrefab.Clone();
        }
        weaponInstance.InitWeapon(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        // finding closes player
        FindClosestPlayer();
        weaponInstance.FixedUpdate();

        if (players.Length > 0 && targetPlayerIndex < players.Length)
        {
            ShootAtPlayer();

            SetNavMeshTarget();
            // face target
            transform.rotation = Quaternion.LookRotation(players[targetPlayerIndex].transform.position - transform.position, Vector3.up);
        }
    }

    private void SetNavMeshTarget()
    {
        distanceToTarget = Vector3.Distance(players[targetPlayerIndex].transform.position, transform.position);
        if (distanceToTarget <= agroRange && distanceToTarget > avoidDistance)
        {
            // start chasing
            navMeshAgent.SetDestination(players[targetPlayerIndex].transform.position);
        }
        else if (distanceToTarget <= avoidDistance)
        {
            // start circling
            Vector3 vectorBetweenTarget = players[targetPlayerIndex].transform.position - transform.position;
            Vector3 newTarget = Quaternion.AngleAxis(rotationAngle, Vector3.up) * vectorBetweenTarget.normalized;
            navMeshAgent.SetDestination(transform.position + newTarget);
        }
        else // random wandering
        {
            navMeshAgent.SetDestination(transform.position + new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f)));
        }
    }

    private void FindClosestPlayer()
    {
        for (int iterator = 0; iterator < players.Length; iterator++)
        {
            if (iterator == 0)
            {
                targetPlayerIndex = 0;
            }
            else if (Vector3.Distance(players[iterator].transform.position, transform.position) < Vector3.Distance(players[targetPlayerIndex].transform.position, transform.position))
            {
                targetPlayerIndex = iterator;
            }
        }
    }

    void ShootAtPlayer()
    {
        if (distanceToTarget <= agroRange)
        {
            weaponInstance.Shoot(players[targetPlayerIndex].transform.position - transform.position);
        }
    }
}
