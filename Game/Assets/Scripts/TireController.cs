using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;

public class TireController : MonoBehaviour
{
    [SerializeField]
    private bool _frontTire;

    private Vector2 _movement;
    private bool _accelerate;
    private bool _decelerate;

    //private GameObject tire;
    private Transform _tireTransform;

    [SerializeField]
    private GameObject _car;
    private Transform _carTransform;
    private Rigidbody _carRB;

    [SerializeField]
    private float _rayCastDistance;

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
    [SerializeField] [Range(0, 1)]
    private float _tireGripFactor;
    [SerializeField]
    private float _tireMass;

    [Header("Acceleration/Deceleration")]
    [SerializeField]
    public bool accelerationTest;
    [SerializeField]
    private float _carSpeed;
    [SerializeField]
    private float _carTopSpeed;
    [SerializeField]
    private AnimationCurve _powerCurve;
    [SerializeField]
    private float _rotationFactor;
    private readonly float _carSpeedMagicMultiplier = 250f;

    private void Awake()
    {
        _tireTransform = transform;

        if (_car == null) _car = transform.parent.parent.gameObject;
        _carTransform = _car.transform;
        _carRB = _car.GetComponent<Rigidbody>();
    }

    public void UpdateInput(Vector2 mov, bool acc, bool dec)
    {
        _movement = mov;
        _accelerate = acc;
        _decelerate = dec;
    }


    private void FixedUpdate()
    {
        RaycastHit tireRay;
        bool rayDidHit = Physics.Raycast(_tireTransform.position, Vector3.down, out tireRay, _rayCastDistance); ;
        Debug.DrawRay(_tireTransform.position, Vector3.down * _rayCastDistance, rayDidHit ? Color.green : Color.red);

        // This is PROBABLY for each TIRE
        // Suspension Spring Force
        if (rayDidHit && suspensionTest)
        {
            if (suspensionTest) Suspension(tireRay);

            if (steeringTest) Steering();

            if (accelerationTest) AccelerateORDecelerate();

        }
    }

    public void SetSuspensionParameters(bool test, float restDist, float strength, float damper, float raycastDist = 0.5f)
    {
        suspensionTest = test;
        _suspensionRestDist = restDist;
        _springStrength = strength;
        _springDamper = damper;
        _rayCastDistance = raycastDist;
    }

    public void SetSteeringParameters(bool test, float angle, float speed, float grip, float mass)
    {
        steeringTest = test;
        _steeringAngle = angle;
        _steeringSpeed = speed;
        _tireGripFactor = grip;
        _tireMass = mass;
    }

    public void SetAccelerationBrakingParameters(bool test, float constSpeed, float topSpeed, AnimationCurve powerCurve, float rotationFactor)
    {
        accelerationTest = test;
        _carSpeed = constSpeed;
        _carTopSpeed = topSpeed;
        _powerCurve = powerCurve;
        _rotationFactor = rotationFactor;
    }


    /* 
     * Suspension Spring Force
     */
    private void Suspension(RaycastHit tireRay)
    {
        // World-space direction of the Spring Force
        Vector3 springDir = _tireTransform.up;

        // World-space velocity of this Tire
        Vector3 tireWorldVel = _carRB.GetPointVelocity(_tireTransform.position);

        // Calculate Offset from the raycast
        float offset = _suspensionRestDist - tireRay.distance;

        // Calculate velocity alone the spring direction
        // Note that springDir is a unit vector, so this returns the magnitude of the tireWorldVel
        // as projected onto springDir
        float vel = Vector3.Dot(springDir, tireWorldVel);

        // Calculate the magnitude of the Dampened Spring Force
        float force = (offset * _springStrength) - (vel * _springDamper);

        // Apply the force at the location of this tire, in the direction of the suspension
        _carRB.AddForceAtPosition(springDir * force, _tireTransform.position);
    }

    /* 
     * Steering Force
     */
    private void Steering()
    {
        if (_frontTire)
        {
            RotateTire();
        }

        // World-space direction of the Spring Force
        Vector3 steeringDir = _tireTransform.right;

        // World-space velocity of the suspension
        Vector3 tireWorldVel = _carRB.GetPointVelocity(_tireTransform.position);

        // What is the tire's velocity in the steering direction?
        // Note that steeringDir is a unit vector, so this returns the magnitude of tireWorldVel
        // as projected onto steeringDir
        float steeringVel = Vector3.Dot(steeringDir, tireWorldVel);

        // The change in the velocity that we're looking for is -steeringVel * gripFactor
        // gripFactor is in range 0-1, 0 means no grip, 1 means full grip
        float desiredVelChange = -steeringVel * _tireGripFactor;

        // Turn change in velocity into an acceleration (acceleration = change in vel / time)
        // This will produce the acceleration necessary to change the velocity by desiredVelChange in 1 physics step
        float desiredAccel = desiredVelChange / Time.fixedDeltaTime;

        // Force = Mass * Acceleration, so multiply by the mass of the tire and apply as a force!
        _carRB.AddForceAtPosition(steeringDir * _tireMass * desiredAccel, _tireTransform.position);

    }

    private void RotateTire()
    {
        // Calculate the desired steering angle based on the input
        float steeringAngle = _movement.x * this._steeringAngle;

        // Calculate the rotation needed to reach the desired steering angle
        Quaternion targetRotation = Quaternion.Euler(0f, steeringAngle, 0f);

        // Rotate the tire towards the target rotation using a smooth interpolation
        _tireTransform.rotation = Quaternion.RotateTowards(_tireTransform.rotation, targetRotation, _steeringSpeed * Time.fixedDeltaTime);
    }

    /* 
     * Acceleration / Braking
     */
    private void AccelerateORDecelerate()
    {
        // World-space direction of the accelerationg/braking force
        Vector3 accelDir = _tireTransform.forward;
        Debug.LogError($"Velocity: {_carRB.velocity}");
        float enginePower = _carSpeed * 100f;

        // Acceleration torque
        if (_accelerate)
        {
            // Forward Speed of the car (in the direction of driving)
            float speed = Vector3.Dot(_carTransform.forward, _carRB.velocity) * _carSpeedMagicMultiplier;

            // Normalized car speed
            float normalizedSpeed = Mathf.Clamp01(Mathf.Abs(speed) / _carTopSpeed);
            // Available torque
            float availableTorque = _powerCurve.Evaluate(normalizedSpeed);

            // Force = Mass * Acceleration, so multiply by the mass of the tire and apply as a force!
            _carRB.AddForceAtPosition(accelDir * availableTorque * enginePower, _tireTransform.position);
            //Debug.LogError($"Abs Speed: {Mathf.Abs(speed)}, TopSpeed: {carTopSpeed}");

            Debug.Log($"Speed: {speed}, Normalized: {normalizedSpeed}, Torque: {availableTorque}, Force: {accelDir * availableTorque * enginePower}");

        }

        if (_decelerate)
        {
            // Backward Speed of the car (in the direction of driving)
            float speed = Vector3.Dot(_carTransform.forward, _carRB.velocity) * _carSpeedMagicMultiplier;

            // Normalized car speed
            float normalizedSpeed = Mathf.Clamp01(Mathf.Abs(speed) / _carTopSpeed);
            // Available torque
            float availableTorque = _powerCurve.Evaluate(normalizedSpeed) * -1 ;

            // Force = Mass * Acceleration, so multiply by the mass of the tire and apply as a force!
            _carRB.AddForceAtPosition(accelDir * availableTorque * enginePower, _tireTransform.position);
            Debug.Log($"Speed: {speed}, Normalized: {normalizedSpeed}, Torque: {availableTorque}, Force: {accelDir * availableTorque * enginePower}");

        }

        //// Calculate the rotation speed based on the car's speed
        //float rotationSpeed = carRB.velocity.magnitude * rotationFactor;

        //// Rotate the tires around their forward axis
        //tireTransform.Rotate(Vector3.right, rotationSpeed * Time.fixedDeltaTime);
    }
}
