using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform spaceShip;

    Vector3 dsp;
    Vector3 smoothDampVelocity;
    public float smoothDampTime = .3f;
    Vector3 camDir;

    Vector3 upDir;
    Vector3 rollVelocity;
    float smoothRollTime = .2f;
    Vector3 smoothPosition;
    Vector3 smoothPositionVelocity;
    float smoothPositionTime = .1f;

    void Start()
    {
        dsp = spaceShip.position - transform.position;
    }

    void Update()
    {
        camDir = Vector3.SmoothDamp(camDir, spaceShip.forward, ref smoothDampVelocity, smoothDampTime);
        upDir = Vector3.SmoothDamp(upDir, spaceShip.up, ref rollVelocity, smoothRollTime);
        transform.rotation = Quaternion.LookRotation(camDir, upDir);

        Vector3 targetPosition = spaceShip.position - transform.forward * 6;
        smoothPosition = Vector3.SmoothDamp(smoothPosition, targetPosition, ref smoothPositionVelocity, smoothPositionTime);
        transform.position = smoothPosition;
    }
}
