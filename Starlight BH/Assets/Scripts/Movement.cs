using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    const string HORIZONTAL_AXIS_NAME = "Horizontal";
    const string VERTICAL_AXIS_NAME = "Vertical";
    [SerializeField] float maxMoveSpeed = 20f;
    [SerializeField] float acceleration = 0.8f;
    [SerializeField] ParticleSystem engineParticles;
    /*[SerializeField]*/ float moveSpeed = 0f;
    Rigidbody myRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // framerate independant update
    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        float horizontalThrow = Input.GetAxisRaw(HORIZONTAL_AXIS_NAME);
        float verticalThrow = Input.GetAxisRaw(VERTICAL_AXIS_NAME);
        float hypothenuse = Mathf.Sqrt(horizontalThrow * horizontalThrow + verticalThrow * verticalThrow);

        float horizontalVelocity;
        float verticalVelocity;

        if(hypothenuse != 0)
        {
            engineParticles.Play();
            moveSpeed += acceleration;
            if(moveSpeed > maxMoveSpeed) moveSpeed = maxMoveSpeed;
        }
        else
        {
            engineParticles.Stop();
            moveSpeed -= acceleration;
            if(moveSpeed < 0) moveSpeed = 0;
        }
        
        if(horizontalThrow != 0)
        {
            horizontalVelocity = horizontalThrow / hypothenuse * moveSpeed;
        }
        else
        {
            horizontalVelocity = myRigidBody.velocity.x * acceleration * acceleration;
        }

        if(verticalThrow != 0)
        {
            verticalVelocity = verticalThrow / hypothenuse * moveSpeed;
        }
        else
        {
            verticalVelocity = myRigidBody.velocity.z * acceleration * acceleration;
        }

        Vector3 playerVelocity = new Vector3(horizontalVelocity, 0, verticalVelocity);
        myRigidBody.velocity = playerVelocity;
    }
}
