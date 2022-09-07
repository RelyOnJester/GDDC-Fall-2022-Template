using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //The other player script is much more refined, but this script attempts movement without a built in controller from Unity

    Rigidbody m_Rigidbody;
    public bool isGrounded = true;
    //Movement variables, adjustable in the unity inspector
    public float speed = 2f;
    public float jumpForce = 2f;

    // Start is called before the first frame update
    void Start()
    {
        //Player's Rigidbody is found at start
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Methods called in update to consistantly function
        Movement();
        Rotation();
    }

    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    //Movement function, allows player to move as WASD are pressed
    void Movement()
    {
        //Checks Unity's inputs for forward and backward, set to WASD
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");


        //During runtime, checks if WASD is being pressed, giving appropriate movement
        //Force is then applied, relative to the player's rotation, and multiplied by the public 'speed' variable
        if (vertical > 0)
        {
            m_Rigidbody.velocity += transform.forward * speed;
        } else if (vertical < 0)
        {
            m_Rigidbody.velocity -= transform.forward * speed;
        } else
        {
            m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, m_Rigidbody.velocity.y, 0);
        }

        if (horizontal > 0)
        {
            m_Rigidbody.velocity += transform.right * speed;
        } else if (horizontal < 0)
        {
            m_Rigidbody.velocity -= transform.right * speed;
        } else
        {
            m_Rigidbody.velocity = new Vector3(0, m_Rigidbody.velocity.y, m_Rigidbody.velocity.z);
        }

        //Player is propelled upward when space is pressed, first checking that player is on ground
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, jumpForce, m_Rigidbody.velocity.z);
            isGrounded = false;
        }

    }

    //Rotation function, rotates player as mouse moves left and right
    //Camera follows player since it is a child of the object. Angle of camera and rotation speed can be manually adjusted
    void Rotation()
    {
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * 2.8f, 0));
    }
}
