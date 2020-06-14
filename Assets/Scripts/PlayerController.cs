using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    [Header("General")]

    // Ship speed in meters per sec
    [Tooltip("In ms^-1")]
    [SerializeField]
    float controlSpeed = 18f;

    // Range of x movement in meters
    [Tooltip("In m")]
    [SerializeField]
    float xRange = 5f;

    // Minimum position from ship position in y
    [Tooltip("In m")]
    [SerializeField]
    float yMin = -3.5f;


    // Maximum position from ship position in y
    [Tooltip("In m")]
    [SerializeField]
    float yMax = 3.5f;

    [Header("Screen-Position Based")]

    [SerializeField]
    float positionPitchFactor = -3f;

    [SerializeField]
    float positionYawFactor = 4f;


    [Header("Control Throw Based")]

    [SerializeField]
    float controlPitchFactor = -20f;

    [SerializeField]
    float controlRollFactor = -25f;

    float xThrow, yThrow;
    bool isControlEnabled = true;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isControlEnabled)
        {
            ProcessTranslation();
            ProcessRotation();
        }
      
    }

    //called by string reference
    private void OnPlayerDeath()
    {
        isControlEnabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("player collided with something");
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Enemy":
                print("hit enemy");
                break;
        }
    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;

        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");


        float xOffset = controlSpeed * xThrow * Time.deltaTime;
        float yOffset = controlSpeed * yThrow * Time.deltaTime;


        float rawXPos = transform.localPosition.x + xOffset;
        float rawYPos = transform.localPosition.y + yOffset;


        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);
        float clampedYPos = Mathf.Clamp(rawYPos, yMin, yMax);

        transform.localPosition = new Vector3(
            clampedXPos,
            clampedYPos,
            transform.localPosition.z
        );
    }
}
