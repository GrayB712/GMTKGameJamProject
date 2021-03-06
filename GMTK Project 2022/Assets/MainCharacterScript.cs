using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterScript : MonoBehaviour
{
    //Rigidbody on the player
    private Rigidbody2D rb;

    //Object on player to check if the player is touching the ground
    public GroundCheck groundCheck;

    //Movement Speed
    public float speed = 10f;

    //Horizontal Input
    private float horizontal = 0f;

    //Vertical Input
    private float vertical = 0f;

    //Vector2 comprised of "vertical" (vertical input) and "horizontal" (horizontal input)
    private Vector2 input;

    //Height of player's jump
    public float jumpAmount = 3f;

    //Gravity of player
    public float gravity = 2f;

    //How quickly the player stops rising in jump
    public float deAcceleration = 1;

    //Whether the player can move all 4 directions or just back and forth, and can jump
    public bool jumpMode = false;

    //Float for keeping track of the gravity variable set in the inspector
    float originalGravity;

    public float speedFall = 3f; 

    public float parabolaEnd = 10f;



    void Start()
    {
        //Stores the original value of gravity as set in the inspector
        originalGravity = gravity;

        //Gets the rigidbody component from player object
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        //Checks if jumpmode was set in inspector -- This part is if jumpmode is off
        if(!jumpMode)
        {
            //check for vertical input
            if(Input.GetKey(KeyCode.W))
            {
                vertical = 1;
            }

            else if(Input.GetKey(KeyCode.S))
            {
                vertical = -1;
            }

            else
            {
                vertical = 0;
            }

            //Check for horizontal input
            if(Input.GetKey(KeyCode.D))
            {
                horizontal = 1;
                transform.rotation = new Quaternion(0, 180, 0, 1);
            }
            else if(Input.GetKey(KeyCode.A))
            {
                horizontal = -1;
                transform.rotation = new Quaternion(0, 0, 0, 1);
            }
            else
            {
                horizontal = 0;
            }
        }

        //Checks if jumpmode was set in inspector -- This part is if jumpmode is on
        if(jumpMode)
        {
            
            //Speed falling with the "S" Key
            if(Input.GetKeyDown(KeyCode.S))
            {
                vertical = 0f;
            }
            if(Input.GetKey(KeyCode.S))
            {
                gravity = originalGravity * speedFall;
                
            }else
            {
                gravity = originalGravity;
            }

            //check for vertical input
            if(Input.GetKeyDown(KeyCode.W) && groundCheck.IsGrounded)
            {
                vertical = jumpAmount;
            }else if(!Input.GetKey(KeyCode.W) && groundCheck.IsGrounded)
            {
                vertical = 0f;
            }

            //Check for horizontal input
            if(Input.GetKey(KeyCode.D))
            {
                horizontal = 1;
                transform.rotation = new Quaternion(0, 180, 0, 1);
            }
            else if(Input.GetKey(KeyCode.A))
            {
                horizontal = -1;
                transform.rotation = new Quaternion(0, 0, 0, 1);
            }
            else
            {
                horizontal = 0;
            }
        }
        
     
        
    }

    private void FixedUpdate()
    {
        //Makes a new Vector 2 for input containing the player's horizontal and vertical input
        input = new Vector2(horizontal, vertical);
        
        //Checks if jumpmode is enabled
        if(jumpMode)
        {
            // //Decreases the player input for jump over time
            // if(vertical > 0)
            // {
            //     vertical -= Time.fixedDeltaTime * deAcceleration;
            // }
            // if(vertical<0)
            // {
            //     vertical = 0;
            // }
            //adds gravity to the Input
            Vector2 theGravity;
            if(groundCheck.IsGrounded)
            {
                theGravity = new Vector2(0f, -.01f);
                input = new Vector2(horizontal, vertical);
                input = input + theGravity;
            }
            if(!groundCheck.IsGrounded)
            {
                theGravity = new Vector2(0f, -(gravity * (Time.time - groundCheck.timeWhenJumped) * (Time.time - groundCheck.timeWhenJumped)));
                input = new Vector2(horizontal, vertical);
                input = input + theGravity;
                if(input.y < 0)
                {
                    input.y = input.y * parabolaEnd;
                }
            }
            
            
            input = new Vector2(input.x * speed, input.y);
        }
        if(!jumpMode)
        {
            input = input * speed;
        }
        
        
        Debug.Log(input);
        //Moves the player to the new calculated position
        rb.MovePosition(rb.position + input * Time.fixedDeltaTime);
    }
}
