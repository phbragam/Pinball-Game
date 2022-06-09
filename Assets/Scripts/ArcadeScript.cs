using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArcadeScript : MonoBehaviour
{
    private ArcadeInputActions arcadeInputActions;
    private InputAction leftPallet;
    private InputAction rightPallet;

    [SerializeField] private GameObject leftPalletObject;
    [SerializeField] private PalleteReference leftPr;
    [SerializeField] private GameObject rightPalletObject;
    [SerializeField] private PalleteReference rightPr;

    [SerializeField] private float initialLeftDegree;
    [SerializeField] private float finalTiltLeftDegree;
    [SerializeField] private float finalPressLeftDegree;
    [SerializeField] private float initialRightDegree;
    [SerializeField] private float finalTiltRightDegree;
    [SerializeField] private float finalPressRightDegree;
    [SerializeField] private float palletTiltSpeed;
    [SerializeField] private float palletPressSpeed;
    [SerializeField] private float palletTreshold;

    private bool leftPalletMoving;
    private bool rightPalletMoving;


    private void Awake()
    {
        arcadeInputActions = new ArcadeInputActions();
    }

    private void OnEnable()
    {
        EnableInputActions();
        leftPr.enabled = false;
        rightPr.enabled = false;
    }

    private void OnDisable()
    {
        DisableInputActions();
    }

    void EnableInputActions()
    {
        leftPallet = arcadeInputActions.Player.LeftPallet;
        rightPallet = arcadeInputActions.Player.RightPallet;

        leftPallet.Enable();
        rightPallet.Enable();

        leftPallet.performed += moveLeftPallet;
        rightPallet.performed += moveRightPallet;
    }

    private void moveRightPallet(InputAction.CallbackContext obj)
    {

        if (!rightPalletMoving)
        {
            if (obj.interaction.GetType().ToString() == "UnityEngine.InputSystem.Interactions.TapInteraction")
            {
                StartCoroutine(movePallet(rightPalletObject, initialRightDegree, finalTiltRightDegree, palletTiltSpeed, false));
            }
            else
            {
                StartCoroutine(movePallet(rightPalletObject, initialRightDegree, finalPressRightDegree, palletPressSpeed, false));
            }
        }
        // Debug.Log("RIGHT PALLET: " + obj.interaction.GetType());
    }

    private void moveLeftPallet(InputAction.CallbackContext obj)
    {
        // if tap move pallet faster and in smaller degree
        // if press move pallet slower and in bigger degree
        if (!leftPalletMoving)
        {
            if (obj.interaction.GetType().ToString() == "UnityEngine.InputSystem.Interactions.TapInteraction")
                StartCoroutine(movePallet(leftPalletObject, initialLeftDegree, finalTiltLeftDegree, -palletTiltSpeed, true));
            else
            {
                StartCoroutine(movePallet(leftPalletObject, initialLeftDegree, finalPressLeftDegree, -palletPressSpeed, true));
            }
        }
        // Debug.Log("LEFT PALLET: " + obj.interaction.GetType());

    }

    void DisableInputActions()
    {
        leftPallet.Disable();
        rightPallet.Disable();

        leftPallet.performed -= moveLeftPallet;
        rightPallet.performed -= moveRightPallet;
    }

    IEnumerator movePallet(GameObject pallet, float initialDegree, float targetDegree, float speed, bool isLeft)
    {

        if (isLeft)
        {
            leftPalletMoving = true;
            leftPr.enabled = true;
        }
        else
        {
            rightPalletMoving = true;
            rightPr.enabled = true;
        }

        while (Mathf.Abs(pallet.transform.eulerAngles.y - targetDegree) > palletTreshold)
        {
            pallet.transform.Rotate(0f, Time.fixedDeltaTime * speed, 0f);
            Mathf.Clamp(pallet.transform.eulerAngles.y, initialDegree, targetDegree);
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }

        // Debug.Log(pallet.transform.eulerAngles.y);

        while (Mathf.Abs(pallet.transform.eulerAngles.y - initialDegree) > palletTreshold)
        {
            pallet.transform.Rotate(0f, -Time.fixedDeltaTime * speed, 0f);
            Mathf.Clamp(pallet.transform.eulerAngles.y, initialDegree, targetDegree);
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }

        if (isLeft)
        {
            leftPalletMoving = false;
            leftPr.enabled = false;
        }
        else
        {
            rightPalletMoving = false;
            rightPr.enabled = false;
        }
    }
}
