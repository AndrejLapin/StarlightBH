using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Projectile[] projectilePrefab; // should probably be a projectile with its own properties and behaviour
    [SerializeField] float damage = 10f;
    [SerializeField] float projectileSpeed = 15f;
    [SerializeField] float shotDelay = 0.5f;
    [SerializeField] int projectileCount = 1; // in one shot
    [SerializeField] int shotsPerBurst = 2;
    [SerializeField] float burstDelay = 0.25f;
    [SerializeField] float accuracyConeRadius = 15f;
    [SerializeField] bool evenSpread = false;

    float spreadStep = 0f;
    float timeAfterLastShot;
    float timeAfterBurst;
    int burstShotsFired;
    Quaternion gunRotation;
    Player myPlayer;

    // Start is called before the first frame update
    void Start()
    {
        
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

        if(shotsPerBurst > 1 && timeAfterBurst >= burstDelay && burstShotsFired < shotsPerBurst)
        {
            LaunchProjectiles();
        }
    }

    public void Shoot(Vector3 direction)
    {
        if(timeAfterLastShot >= shotDelay)
        {
            gunRotation = Quaternion.LookRotation(direction, Vector3.up);
            burstShotsFired = 0;

            LaunchProjectiles();
            
            timeAfterLastShot = 0;
        }
    }

    public void InitWeapon(Player player)
    {
        myPlayer = player;
        timeAfterLastShot = shotDelay;
        timeAfterBurst = burstDelay;
        burstShotsFired = shotsPerBurst;

        if(evenSpread)
        {
            spreadStep = accuracyConeRadius * 2 / (projectileCount-1);
        }
    }

    void LaunchProjectiles()
    {
        for(int iterator = 0; iterator < projectileCount; iterator++)
        {
                Projectile gunProjectile = Instantiate(projectilePrefab[iterator % projectilePrefab.Length], myPlayer.GetPlayerPosition(), gunRotation) as Projectile;
                Rigidbody projectileRigidBody = gunProjectile.GetComponent<Rigidbody>();
                if(evenSpread)
                {
                    projectileRigidBody.transform.rotation *= Quaternion.Euler(0, iterator * spreadStep - accuracyConeRadius, 0); 
                }
                else
                {
                    projectileRigidBody.transform.rotation *= Quaternion.Euler(0, Random.Range(-accuracyConeRadius, accuracyConeRadius), 0); 
                }
                projectileRigidBody.velocity = projectileRigidBody.transform.forward * projectileSpeed;
        }
        timeAfterBurst = 0;
        burstShotsFired++;
    }
}
