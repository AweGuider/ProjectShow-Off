using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    [SerializeField] private Rigidbody _rb;

    public void Init(float velocity) {
        _rb.velocity = transform.forward * velocity;
    }
}
