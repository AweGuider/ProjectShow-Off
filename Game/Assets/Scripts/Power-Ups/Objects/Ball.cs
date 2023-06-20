using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CustomGravity))]
public class Ball : MonoBehaviour {

    [SerializeField]
    private Rigidbody _rb;

    [SerializeField]
    private CustomGravity _gravity;

    [SerializeField]
    ParticleSystem _explosion;

    [SerializeField]
    public float GravityScale { get => _gravity.gravityScale; } // Adjust this value for different gravity effects


    public void Init(float velocity) {
        _rb.velocity = transform.forward * velocity;
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_explosion != null) _explosion.Play();
        Destroy(gameObject);
    }
}
