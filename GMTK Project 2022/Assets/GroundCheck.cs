using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool IsGrounded = false;

    public float timeWhenJumped = 0f;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Ground")
        {
            IsGrounded = true;

        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.tag == "Ground")
        {
            IsGrounded = false;
            timeWhenJumped = Time.time;
        }
    }

}
