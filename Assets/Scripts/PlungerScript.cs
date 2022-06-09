using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlungerScript : MonoBehaviour
{
    private ArcadeInputActions arcadeInputActions;
    private InputAction spring;
    public float power;
    public float maxPower = 100f;
    public Slider powerSlider;

    public Vector3 direction;

    public Rigidbody ballRb;


    private void Awake()
    {
        arcadeInputActions = new ArcadeInputActions();
    }

    private void OnEnable()
    {
        spring = arcadeInputActions.Player.Spring;
        spring.Enable();
        spring.performed += HitBall;
    }

    private void OnDisable()
    {
        spring = arcadeInputActions.Player.Spring;
        spring.Disable();
        spring.performed += HitBall;
    }

    private void Update()
    {

    }

    private void HitBall(InputAction.CallbackContext obj)
    {

        if (ballRb)
        {
            ballRb.AddForce(maxPower * direction);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<BallScript>())
        {
            ballRb = other.gameObject.GetComponent<Rigidbody>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<BallScript>())
        {
            ballRb = null;
        }
    }
}
