using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Movement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    static const string HORIZONTAL_AXIS_NAME = "Horizontal";
    static const string VERTICAL_AXIS_NAME = "Vertical";

    RigidBody2D myRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // frame rate independant update
    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        float horizontalThrow = CrossPlatformInputManager.GetAxis(HORIZONTAL_AXIS_NAME);
        float verticalThrow = CrossPlatformInputManager.GetAxis(VERTICAL_AXIS_NAME);

        Vector3 playerVelocity = new Vector3(horizontalThrow * moveSpeed, 0, verticalThrow * moveSpeed); // need to get absolute vector
        
        myRigidBody.velocity = playerVelocity;
    }
}
