using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpVisuals : MonoBehaviour
{
    public Transform xRotation;
    public Transform yRotation;
    public GroundedCheck groundedScript;

    public float xRotationSpeed = 360;
    public float yRotationSpeed = 1440;

    public bool grounded;

	private void FixedUpdate()
	{
        if (groundedScript != null)
        {
            grounded = groundedScript.grounded;
        }
	}

	private void Update()
    {
        if (!grounded)
        {
            yRotation.Rotate(0, Time.deltaTime * yRotationSpeed, 0);
            xRotation.Rotate(Time.deltaTime * xRotationSpeed, 0, 0);
        }
        else
        {
            yRotation.localRotation = Quaternion.identity;
            xRotation.localRotation = Quaternion.identity;
        }
    }
}
