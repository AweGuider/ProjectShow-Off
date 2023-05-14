using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartControllerOwn : MonoBehaviour
{
    public float driftForce = 10f;      // The force applied for drifting
    public float maxDriftAngle = 45f;   // The maximum angle at which the kart can drift
    public float driftControl = 1f;     // Adjust the drifting control sensitivity

    private bool isDrifting = false;    // Flag to track if the kart is currently drifting

    public float acceleration = 10f;         // The acceleration of the kart
    public float maxSpeed = 20f;             // The maximum speed the kart can reach
    public float steeringSpeed = 10f;        // The speed at which the kart steers
    public float reverseMultiplier = 0.5f;   // The multiplier applied to acceleration when moving in reverse


    private Rigidbody kartRigidbody;

    private void Awake()
    {
        kartRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Check if the drift button is pressed
        if (Input.GetButtonDown("Drift"))
        {
            isDrifting = true;
        }
        // Check if the drift button is released
        else if (Input.GetButtonUp("Drift"))
        {
            isDrifting = false;
        }
    }

    private void FixedUpdate()
    {
        // Apply drifting force when the kart is drifting
        if (isDrifting)
        {
            float driftAngle = Input.GetAxis("Horizontal") * maxDriftAngle;
            Vector3 driftForce = Quaternion.Euler(0f, driftAngle, 0f) * transform.forward * this.driftForce;
            kartRigidbody.AddForce(driftForce, ForceMode.Force);
        }

        // Get the horizontal input axis for steering
        float steeringInput = Input.GetAxis("Horizontal");

        // Get the vertical input axis for acceleration
        float accelerationInput = Input.GetAxis("Vertical");

        // Apply acceleration to the kart
        float currentSpeed = kartRigidbody.velocity.magnitude;
        Vector3 desiredVelocity = acceleration * accelerationInput * transform.forward;

        // Adjust acceleration when moving in reverse
        if (accelerationInput < 0f)
        {
            desiredVelocity *= reverseMultiplier;
        }

        // Limit the maximum speed of the kart
        if (currentSpeed > maxSpeed)
        {
            desiredVelocity *= maxSpeed / currentSpeed;
        }

        // Apply forces to move the kart
        float forceFactor = Mathf.Clamp01(1f - currentSpeed / maxSpeed);
        kartRigidbody.AddForce(forceFactor * desiredVelocity, ForceMode.VelocityChange);
        //kartRigidbody.AddForce(forceFactor * desiredVelocity, ForceMode.Impulse);


        // Rotate the kart based on steering input
        Quaternion rotation = Quaternion.Euler(0f, steeringInput * steeringSpeed * Time.fixedDeltaTime, 0f);
        kartRigidbody.MoveRotation(kartRigidbody.rotation * rotation);
    }
}

