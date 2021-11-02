using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{   
    [SerializeField] float projectileDamage = 10f;
    [SerializeField] float lifeTimeSec = 5f;
    BoxCollider myCollider;

    float currentLifetime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // framerate independant update
    void FixedUpdate()
    {
        currentLifetime += Time.deltaTime;
        if(currentLifetime >= lifeTimeSec)
        {
            Destroy(gameObject);
        }
    }

    public void SetLifeTime(float newLifeTime)
    {
        lifeTimeSec = newLifeTime;
    }

    public void SetDamage(float newDamage)
    {
        projectileDamage = newDamage;
    }

    private void OnTriggerEnter(Collider collision)
    {
        Health healthComponent = collision.GetComponent<Health>();
        if(healthComponent)
        {
            healthComponent.SubtracktHealth(projectileDamage);
        }
        Destroy(gameObject);
    }
}
