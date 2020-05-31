using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    // Ship speed in meters per sec
    [Tooltip("In ms^-1")]
    [SerializeField]
    float speed = 5f;

    // Range of x movement in meters
    [Tooltip("In m")]
    [SerializeField]
    float xRange = 5f;

    // Minimum position from ship position in y
    [Tooltip("In m")]
    [SerializeField]
    float yMin = -5f;


    // Maximum position from ship position in y
    [Tooltip("In m")]
    [SerializeField]
    float yMax = 5f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //doit etre géré avec editor / edit / project settings / input manager

        //if the gamepad joystick is centered there is no throw.
        //if it is 100% the right throw of 1 if 100% to the left could be -1
        //quand on push une touche du clavier le throw va de 0 à 1 ou -1 mais pas instantanement
        //selon le settings de gravity et de sensitivity
        // avec une manette c'est en fonction du pourcentage par rapport au déplacement maximal
        float xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float yThrow = CrossPlatformInputManager.GetAxis("Vertical");


        float xOffset = speed * xThrow * Time.deltaTime;
        float yOffset = speed * yThrow * Time.deltaTime;


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
