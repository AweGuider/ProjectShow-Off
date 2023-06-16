using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {

    [SerializeField]
    private Ball _ballPrefab;

    [SerializeField]
    private Transform _ballSpawn;

    [SerializeField]
    private float _velocity = 10;


    public void FireTheBall(float kartVelocity, float multiplier)
    {
        var ball = Instantiate(_ballPrefab, _ballSpawn.position, _ballSpawn.rotation);
        ball.Init((_velocity + kartVelocity) * multiplier);
    }
}
