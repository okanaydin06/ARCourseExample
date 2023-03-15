using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    public Joystick joystick;
    public float speed = 10;
    private Vector3 velocityVector = Vector3.zero;

    public float maxVelocityChange = 10f;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float _xMovementInput = joystick.Horizontal;
        float _zMovementInput = joystick.Vertical;

        Vector3 _movementHorizontal = transform.right * _xMovementInput;
        Vector3 _movementVertical = transform.forward * _zMovementInput;

        Vector3 _movementVelocityVector = (_movementHorizontal + _movementVertical).normalized * speed;

        Move(_movementVelocityVector);
    }

    void Move(Vector3 movementVelocityVector)
    {
        velocityVector = movementVelocityVector;
    }

    private void FixedUpdate()
    {
        if(velocityVector != Vector3.zero)
        {
            Vector3 velocity = rb.velocity;
            Vector3 velocityChange = (velocityVector - velocity);

            velocityChange.x = Math.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Math.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);


            rb.AddForce(velocityChange, ForceMode.Acceleration);



        }
    }

}
