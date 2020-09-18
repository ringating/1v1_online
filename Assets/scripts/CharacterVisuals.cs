using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterVisuals : MonoBehaviour
{
    public Transform visualsTransform;
    public Animator characterAnimator;
    public Rigidbody characterRigidBody;

    public float moveAnimationSpeedScalar = 1;

    private Vector3 horizontalVel = new Vector3();
    private bool moving;
    private float moveSpeedThreshold = 0.01f;

    // Update is called once per frame
    void FixedUpdate()
    {
        horizontalVel.Set(characterRigidBody.velocity.x, 0, characterRigidBody.velocity.z);

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

        if (moving)
        {
            visualsTransform.LookAt(visualsTransform.position + horizontalVel);
            characterAnimator.SetFloat("speed", moveAnimationSpeedScalar * horizontalVel.magnitude);
        }
    }
}
