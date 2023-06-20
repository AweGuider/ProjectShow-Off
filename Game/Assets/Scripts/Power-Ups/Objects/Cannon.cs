using KartGame.KartSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Cannon : MonoBehaviour {

    [SerializeField]
    CannonPowerUp _powerUp;

    [SerializeField]
    private Ball _ballPrefab;

    [SerializeField]
    private Transform _ballSpawn;

    [SerializeField]
    private float _velocity = 10;

    [SerializeField]
    private int _trajectoryResolution = 30; // Number of points in the trajectory line

    private LineRenderer _aimingPointer; // Reference to the Line Renderer component

    private void Awake()
    {
        _aimingPointer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        // Calculate the trajectory points
        Vector3[] trajectoryPoints = CalculateTrajectory();

        // Set the positions of the Line Renderer
        _aimingPointer.positionCount = trajectoryPoints.Length;
        _aimingPointer.SetPositions(trajectoryPoints);
    }

    private Vector3[] CalculateTrajectory()
    {
        Vector3[] points = new Vector3[_trajectoryResolution];

        // Get the initial position and velocity of the ball
        Vector3 initialPosition = _ballSpawn.position;

        float sumVelocity = (_velocity + _powerUp.GetKart().Rigidbody.velocity.magnitude) * _powerUp.FirePowerMultiplier;

        Vector3 initialVelocity = _ballSpawn.forward * Mathf.Clamp(sumVelocity, 13, 20); // Modify this calculation based on your requirements
        //Mathf.Lerp
        Debug.Log(initialVelocity.magnitude);
        float timeStep = Time.fixedDeltaTime;

        Vector3 initialGravity = Physics.gravity * _ballPrefab.GravityScale;

        // Calculate the trajectory points
        for (int i = 0; i < _trajectoryResolution; i++)
        {
            float time = i * timeStep;
            points[i] = initialPosition + initialVelocity * time + 0.5f * initialGravity * time * time;
        }

        return points;
    }

    public void FireTheBall(float kartVelocity, float multiplier)
    {
        var ball = Instantiate(_ballPrefab, _ballSpawn.position, _ballSpawn.rotation);
        ball.Init((_velocity + kartVelocity) * multiplier);

    }
}
