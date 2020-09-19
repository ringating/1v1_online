using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterVisuals : MonoBehaviour
{
    public Transform visualsTransform;
    public Animator characterAnimator;
    public Rigidbody characterRigidBody;
    public JumpVisuals jumpVisualsScript;

    public Vector3 jumpPositionOffset;

    public float moveAnimationSpeedScalar = 1;

    private Vector3 horizontalVel = new Vector3();
    private bool moving;
    private float moveSpeedThreshold = 0.01f;
    private bool wasGrounded = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        horizontalVel.Set(characterRigidBody.velocity.x, 0, characterRigidBody.velocity.z);

        if (jumpVisualsScript.grounded)
        {
            if (!moving && horizontalVel.magnitude > moveSpeedThreshold)
            {
                characterAnimator.SetTrigger("move");
                moving = true;
            }
            else if (moving && horizontalVel.magnitude <= moveSpeedThreshold)
            {
                characterAnimator.SetTrigger("idle");
                moving = false;
            }
        }
        else { moving = false; }

        if (moving || !jumpVisualsScript.grounded)
        {
            visualsTransform.LookAt(visualsTransform.position + horizontalVel);
            characterAnimator.SetFloat("speed", moveAnimationSpeedScalar * horizontalVel.magnitude);
        }

        if (!jumpVisualsScript.grounded && wasGrounded)
        {
            // begin jump
            jumpVisualsScript.yRotation.localPosition = jumpPositionOffset;
            characterAnimator.SetTrigger("jump");
        }
        else if (jumpVisualsScript.grounded && !wasGrounded)
        {
            // end jump
            jumpVisualsScript.yRotation.localPosition = Vector3.zero;
            characterAnimator.SetTrigger("idle");
            moving = false;
        }

        wasGrounded = jumpVisualsScript.grounded;
    }
}
