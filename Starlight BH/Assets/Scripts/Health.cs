using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f;
    [SerializeField] bool invulnarable = false;
    public float health;


    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() 
    {
        if(health <= 0)
        {
            // TODO: fix player death
            Destroy(gameObject);
        }
    }

    public void SubtracktHealth(float amount)
    {
        if(!invulnarable)
        {
            health -= amount;
        }
    }
}
