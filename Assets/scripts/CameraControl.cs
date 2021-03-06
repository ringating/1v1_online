﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform cameraTransform;
    public Transform anchorPointTransform;
    public float followDistance = 5f;
    public float mouseSensitivity = 1f;
    public float controllerSensitivity = 1f;

    private float pitchDegrees = 0;
    private float yawDegrees = 0;

    private Vector3 cameraWorldPosition = new Vector3();

    void LateUpdate()
    {
        // mouse
        pitchDegrees = Mathf.Max(-89f, Mathf.Min(89f, pitchDegrees + mouseSensitivity * -Input.GetAxis("Mouse Y")));
        yawDegrees = (yawDegrees + mouseSensitivity * Input.GetAxis("Mouse X")) % 360;

        // controller
        pitchDegrees = Mathf.Max(-89f, Mathf.Min(89f, pitchDegrees + controllerSensitivity * Input.GetAxisRaw("Look Vertical")));
        yawDegrees = (yawDegrees + controllerSensitivity * Input.GetAxisRaw("Look Horizontal")) % 360;

        cameraWorldPosition.Set(anchorPointTransform.position.x, anchorPointTransform.position.y, anchorPointTransform.position.z - followDistance);
        cameraTransform.SetPositionAndRotation(cameraWorldPosition, Quaternion.identity);

        cameraTransform.RotateAround(anchorPointTransform.position, Vector3.right, pitchDegrees);
        cameraTransform.RotateAround(anchorPointTransform.position, Vector3.up, yawDegrees);
    }
}
