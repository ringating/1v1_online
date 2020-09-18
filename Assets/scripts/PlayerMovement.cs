using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1;
    public Rigidbody rb;

    private Vector3 vel = new Vector3();
    private float OutOfBoundsHeight = -5f;
    private Vector3 resetPosition = new Vector3(0,8,0);

    // Update is called once per frame
    void FixedUpdate()
    {
        vel.Set(
            Input.GetAxisRaw("Horizontal") * speed,
            rb.velocity.y,
            Input.GetAxisRaw("Vertical") * speed
        );
        rb.velocity = vel;

        if (rb.position.y < OutOfBoundsHeight) 
        {
            rb.position = resetPosition;
        }
    }
}
