using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarBodyController : MonoBehaviour
{
    [SerializeField]
    private Vector2 movement;
    [SerializeField]
    private bool accelerate;
    [SerializeField]
    private bool decelerate;

    [SerializeField]
    private List<TireController> tires;

    private Rect movementRect;
    private Rect accelerationRect;
    private Rect decelerationRect;

    [Header("Suspension")]
    [SerializeField]
    public bool suspensionTest;
    [SerializeField]
    private float _suspensionRestDist;
    [SerializeField]
    private float _springStrength;
    [SerializeField]
    private float _springDamper;

    [Header("Steering")]
    [SerializeField]
    public bool steeringTest;
    [SerializeField]
    private float _steeringAngle;
    [SerializeField]
    private float _steeringSpeed;
    [SerializeField]
    [Range(0, 1)]
    private float _tireGripFactor;
    [SerializeField]
    private float _tireMass;

    [Header("Acceleration/Deceleration")]
    [SerializeField]
    public bool accelerationTest;
    [SerializeField]
    private float _carSpeedMultiplier;
    [SerializeField]
    private float _carTopSpeed;
    [SerializeField]
    private AnimationCurve _powerCurve;
    [SerializeField]
    private float _rotationFactor;

    private void Awake()
    {
        if (tires == null || tires.Count == 0)
        {
            tires = new();
            foreach (Transform tire in transform.GetChild(0).transform)
            {
                tires.Add(tire.GetComponent<TireController>());
            }
        }

    }

    private void Start()
    {
        movementRect = new Rect(50, 50, 200, 40);
        accelerationRect = new Rect(50, 100, 200, 40);
        decelerationRect = new Rect(50, 150, 200, 40);

        foreach (TireController tire in tires)
        {
            tire.SetSuspensionParameters(suspensionTest, _suspensionRestDist, _springStrength, _springDamper);
            tire.SetSteeringParameters(steeringTest, _steeringAngle, _steeringSpeed, _tireGripFactor, _tireMass);
            tire.SetAccelerationBrakingParameters(accelerationTest, _carSpeedMultiplier, _carTopSpeed, _powerCurve, _rotationFactor);
        }

    }

    private void FixedUpdate()
    {
        Debug.DrawRay(transform.position, Vector3.forward * 1, Color.blue);
        Debug.DrawRay(transform.position, Vector3.right * 1, Color.red);
        Debug.DrawRay(transform.position, Vector3.up * 1, Color.yellow);

        UpdateTiresInput();
    }

    public void OnMove(InputAction.CallbackContext context) => movement = context.ReadValue<Vector2>();
    public void OnAccelerate(InputAction.CallbackContext context) => accelerate = context.action.triggered;
    public void OnDecelerate(InputAction.CallbackContext context) => decelerate = context.action.triggered;


    private void OnGUI()
    {
        GUIStyle labelStyle = GUI.skin.label; // Use the default label style
        labelStyle.wordWrap = true; // Enable word wrap

        GUI.TextArea(movementRect, $"Movement: X:{movement.x}, Y: {movement.y}");
        GUI.TextArea(accelerationRect, $"Acceleration: {accelerate}");
        GUI.TextArea(decelerationRect, $"Deceleration: {decelerate}");
    }



    private void UpdateTiresInput()
    {
        foreach (TireController tire in tires)
        {
            tire.UpdateInput(movement, accelerate, decelerate);
        }
    }
}
