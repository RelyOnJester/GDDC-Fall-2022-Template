using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    //This script manages player movement by using a built in Character Controller from Unity

    //Variables, many adjustable from Unity's inspector
    public CharacterController controller;
    public Transform camera;
    public float speed = 1f;
    float horizontal;
    float vertical;
    Vector3 direction;
    public float gravityStrength = 1f;
    public Rigidbody rb;
    public float jumpForce = 1f;
    Vector3 jumping;

    //Vars to check for ground below player
    public Transform grounded;
    public float belowDistance = .5f;
    public LayerMask groundMask;
    public bool isGrounded = true;
    
    void Start()
    {
        //Rigidbody is assigned when program starts
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        //When a sphere attached to the player collides with something, the bool is flipped to say the player is on the ground
        isGrounded = Physics.CheckSphere(grounded.position, belowDistance, groundMask);

        //If statement to stabilize player's movement along the Y-axis, otherwise gravity could keep increasing
        if (isGrounded && jumping.y < 0)
        {
            jumping.y = -2f;
        }

        //Checks for directional inputs, usually WASD
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        direction = new Vector3(horizontal, rb.velocity.y, vertical);

        //Player inputs are transferred onto player character, relative to camera
        if (direction.magnitude >= 0.1f)
        {
            Vector3 moveDire = Quaternion.Euler(0f, camera.eulerAngles.y, 0f) * direction;
            controller.Move(moveDire * speed * Time.deltaTime);
        }

        //While player is on the ground, space will let them jump
        if (Input.GetKeyDown("space") && isGrounded)
        {
            jumping.y = Mathf.Sqrt(jumpForce * 2 * gravityStrength);
        }

        //Gravity applied via script. The Rigidbody also allows for the use of gravity
        jumping.y -= gravityStrength * Time.deltaTime;
        controller.Move(jumping * Time.deltaTime);

        //The camera will rotate as the mouse turns left and right
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * 2.8f, 0));
    }

    
}
