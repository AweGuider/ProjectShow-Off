using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    [SerializeField]
    private Rigidbody _rb;

    [SerializeField]
    ParticleSystem _explosion;

    public void Init(float velocity) {
        _rb.velocity = transform.forward * velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_explosion != null) _explosion.Play();
        Destroy(gameObject);
    }
}
