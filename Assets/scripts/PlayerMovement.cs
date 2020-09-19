using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1;
    public float jumpSpeed = 5;
    public float gravityAcceleration = 9.8f;
    public Rigidbody rb;
    public CameraControl cameraScript;
    public GroundedCheck groundedScript;

    private Vector3 vel = new Vector3();
    private float OutOfBoundsHeight = -5f;
    private Vector3 resetPosition = new Vector3(0,8,0);
    private Vector3 gravityVector = new Vector3();

    private Vector3 currForward = new Vector3();
    private Vector3 currRight = new Vector3();

	private void Start()
	{
        rb.useGravity = false;
	}

	void FixedUpdate()
    {
        currForward.Set(
            cameraScript.anchorPointTransform.position.x - cameraScript.cameraTransform.position.x,
            0,
            cameraScript.anchorPointTransform.position.z - cameraScript.cameraTransform.position.z
        );
        currRight = Vector3.Cross(currForward, Vector3.up);

        vel = currForward.normalized * Input.GetAxisRaw("Vertical") * speed - currRight.normalized * Input.GetAxisRaw("Horizontal") * speed;
        vel = Vector3.ClampMagnitude(vel, speed);
        vel.y = rb.velocity.y;

        rb.velocity = vel;

        if (rb.position.y < OutOfBoundsHeight) 
        {
            rb.position = resetPosition;
        }

        if (groundedScript.grounded && Input.GetButton("Jump"))
        {
            rb.velocity = new Vector3(
                rb.velocity.x,
                jumpSpeed,
                rb.velocity.z
            );
        }

        gravityVector.Set(0, -gravityAcceleration, 0);
        rb.AddForce(gravityVector, ForceMode.Acceleration);
    }
}
