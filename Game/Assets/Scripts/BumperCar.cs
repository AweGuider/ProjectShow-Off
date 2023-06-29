using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BumperCar : MonoBehaviour
{
    public Transform[] travelPoints;
    public float travelSpeed = 50f;
    public float rotationSpeed = 100f;

    private int currentPointIndex = 0;

    private void Start()
    {
        rotationSpeed = Random.Range(50f, 100f);
        travelSpeed = Random.Range(35f, 65f);
        transform.position = travelPoints[0].position;
    }

    private void Update()
    {
        // Move towards the current travel point
        transform.position = Vector3.MoveTowards(transform.position, travelPoints[currentPointIndex].position, travelSpeed * Time.deltaTime);

        //// Calculate the rotation amount based on rotationSpeed
        //float rotationAmount = rotationSpeed * Time.deltaTime;

        //// Apply continuous rotation around the Y-axis
        //transform.Rotate(Vector3.up, rotationAmount);

        // Rotate towards the current travel point
        Quaternion targetRotation = Quaternion.LookRotation(travelPoints[currentPointIndex].position - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Check if reached the current travel point
        if (transform.position == travelPoints[currentPointIndex].position)
        {
            // Move to the next travel point
            currentPointIndex = (currentPointIndex + 1) % travelPoints.Length;
        }
    }

    //public float forwardForce = 10f;
    //public float rotationSpeed = 2f;

    //private Rigidbody carRigidbody;

    //private void Awake()
    //{
    //    carRigidbody = GetComponent<Rigidbody>();
    //}

    //private void FixedUpdate()
    //{
    //    // Apply forward force
    //    carRigidbody.AddForce(transform.forward * forwardForce, ForceMode.Force);

    //    // Apply slight rotation
    //    carRigidbody.angularVelocity = new Vector3(0f, rotationSpeed, 0f);
    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    // Check if the bumper car collided with another collider
    //    if (!collision.collider.CompareTag("BumperCar"))
    //    {
    //        // Calculate the new direction after collision
    //        Vector3 newDirection = Vector3.Reflect(carRigidbody.velocity.normalized, collision.contacts[0].normal);

    //        // Apply the new direction to the bumper car's velocity
    //        carRigidbody.velocity = newDirection * carRigidbody.velocity.magnitude;
    //    }
    //}
}
