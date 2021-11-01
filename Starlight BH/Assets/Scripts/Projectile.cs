using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{   
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

    private void OnTriggerEnter(Collider collision)
    {
        Destroy(gameObject);
    }
}
