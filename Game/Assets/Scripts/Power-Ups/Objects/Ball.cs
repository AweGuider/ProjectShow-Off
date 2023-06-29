using KartGame.KartSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CustomGravity))]
public class Ball : OnTriggerArcadeKartPowerup
{
    public ArcadeKart kart;

    [SerializeField]
    private Rigidbody _rb;

    [SerializeField]
    private CustomGravity _gravity;

    [SerializeField]
    int radius;

    [SerializeField]
    int force;
    [SerializeField]
    ParticleSystem _explosion;

    private SphereCollider sphere;

    [SerializeField]
    public float GravityScale { get => _gravity.gravityScale; } // Adjust this value for different gravity effects

    private void Start()
    {
        _explosion.gameObject.SetActive(false);
    }
    
    public void SetKart(ArcadeKart trigger)
    {
        kart = trigger;
    }

    public void Init(float velocity) {
        _rb.velocity = transform.forward * velocity;
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.name);
        
        if (collision.gameObject.TryGetComponent(out IKart IKart))
        {
            ArcadeKart IKartObj = (ArcadeKart)IKart;
            if (kart == null || kart == IKartObj)
            {
                Destroy(gameObject);
                return;
            }

            IKart.ReactToExplosion(force, transform.position, radius);
            //Debug.Log(kart.name);

        }
        if (_explosion != null)
        {
            _explosion.gameObject.SetActive(true);
        }
        Instantiate(_explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
