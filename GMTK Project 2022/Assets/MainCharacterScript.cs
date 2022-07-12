using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 10f;
    private float horizontal = 0f;
    private float vertical = 0f;
    private Vector2 input;

    public float jumpAmount = 3f;

    public float gravity = 2f;

    public float deAcceleration = 1;

    public bool jump = false;

    float originalGravity;



    void Start()
    {
        originalGravity = gravity;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        if(Input.GetKey(KeyCode.S))
        {
            gravity = gravity * 2;
        }else
        {
            gravity = originalGravity;
        }

        if(!jump)
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

        if(jump)
        {
            //check for vertical input
            if(Input.GetKeyDown(KeyCode.W))
            {
                vertical = jumpAmount;
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
        if(jump && vertical > 0)
        {
            vertical -= Time.fixedDeltaTime * deAcceleration;
        }
        if(vertical<0)
        {
            vertical = 0;
        }
        input = new Vector2(horizontal, vertical);
        rb.MovePosition(rb.position + input * Time.fixedDeltaTime * speed);
        //Debug.Log("vertical:" + vertical + "  horizontal" + horizontal);

        //Gravity
        if(vertical < .01f)
        {
            Vector2 theGravity = new Vector2(0f, -gravity);
            rb.MovePosition(rb.position + theGravity * Time.fixedDeltaTime);
        }

    }
}
