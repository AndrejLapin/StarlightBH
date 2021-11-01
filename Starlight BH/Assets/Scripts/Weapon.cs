using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Projectile projectilePrefab; // should probably be a projectile with its own properties and behaviour
    [SerializeField] float damage = 10f;
    [SerializeField] float projectileSpeed = 15f;
    [SerializeField] float shotDelay = 0.5f;
    [SerializeField] int projectileCount = 1; // in one shot
    [SerializeField] int shotsPerBurst = 2;
    [SerializeField] float burstDelay = 0.25f;
    [SerializeField] float accuracyConeRadius = 15f;
    [SerializeField] bool evenSpread = false;

    float timeAfterLastShot;
    float timeAfterBurst;
    int burstShotsFired;
    Quaternion gunRotation;

    Player myPlayer;

    // Start is called before the first frame update
    void Start()
    {
        myPlayer = gameObject.GetComponentInParent(typeof(Player)) as Player;
        timeAfterLastShot = shotDelay;
        timeAfterBurst = burstDelay;
        burstShotsFired = shotsPerBurst;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // framerate independant update
    public void FixedUpdate()
    {
        if(timeAfterLastShot <= shotDelay)
        {
            timeAfterLastShot += Time.deltaTime;
        }

        if(shotsPerBurst > 1 && timeAfterBurst <= burstDelay)
        {
            timeAfterBurst += Time.deltaTime;
        }

        if(shotsPerBurst > 1 && timeAfterBurst >= burstDelay)
        {

        }
    }

    public void Shoot(Vector3 direction)
    {
        if(timeAfterLastShot >= shotDelay)
        {
            gunRotation = Quaternion.LookRotation(direction, Vector3.up);

            LaunchProjectiles();
            
            timeAfterLastShot = 0;
        }
    }

    void LaunchProjectiles()
    {
        for(int iterator = 1; iterator <= projectileCount; iterator++)
            {
                Projectile gunProjectile = Instantiate(projectilePrefab, myPlayer.GetPlayerPosition(), gunRotation) as Projectile;
                Rigidbody projectileRigidBody = gunProjectile.GetComponent<Rigidbody>();
                projectileRigidBody.transform.rotation *= Quaternion.Euler(0, Random.Range(-accuracyConeRadius, accuracyConeRadius), 0); 
                projectileRigidBody.velocity = projectileRigidBody.transform.forward * projectileSpeed;
                timeAfterBurst = 0;
            }
    }
}
