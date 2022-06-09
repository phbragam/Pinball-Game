using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlungerScript : MonoBehaviour
{
    private ArcadeInputActions arcadeInputActions;
    private InputAction spring;
    [SerializeField] float power;
    [SerializeField] float minPower = 0f;
    [SerializeField] float maxPower = 1000f;

    [SerializeField] float barFillSpeed;

    public Slider powerSlider;

    public Vector3 direction;

    public Rigidbody ballRb;


    private void Awake()
    {
        arcadeInputActions = new ArcadeInputActions();
        powerSlider.minValue = minPower;
        powerSlider.maxValue = maxPower;
    }

    private void OnEnable()
    {
        spring = arcadeInputActions.Player.Spring;
        spring.Enable();
    }

    private void OnDisable()
    {
        spring = arcadeInputActions.Player.Spring;
        spring.Disable();
    }

    private void Update()
    {
        if (ballRb)
        {
            powerSlider.value = power;
            if (spring.IsPressed())
            {
                if (power <= maxPower)
                {
                    power += barFillSpeed * Time.deltaTime;
                }
            }

            if (spring.WasReleasedThisFrame())
            {

                HitBall();
            }
        }
        else
        {
            power = minPower;
        }

    }

    private void HitBall()
    {

        if (ballRb)
        {
            ballRb.AddForce(power * direction);
            power = minPower;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<BallScript>())
        {
            powerSlider.gameObject.SetActive(true);
            ballRb = other.gameObject.GetComponent<Rigidbody>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<BallScript>())
        {
            powerSlider.gameObject.SetActive(false);
            ballRb = null;
        }
    }
}
