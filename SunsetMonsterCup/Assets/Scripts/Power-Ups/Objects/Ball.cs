using KartGame.KartSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CustomGravity))]
public class Ball : OnTriggerArcadeKartPowerup
{
    public ArcadeKart kart;
    public KartTrigger trigger;

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

    public void SetTrigger(KartTrigger trigger)
    {
        this.trigger = trigger;
    }

    public void Init(float velocity) {
        _rb.velocity = transform.forward * velocity;
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.name);
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        //Debug.Log(colliders.Length);
        foreach (Collider collider in colliders)
        {
            //// For kart collider
            //if (collider.TryGetComponent(out IKart affectedKart))
            //{
            //    ArcadeKart IKartObj = (ArcadeKart)affectedKart;
            //    if (kart == null || kart == IKartObj)
            //    {
            //        //Destroy(gameObject);
            //        return;
            //    }

            //    Debug.Log($"Kart Obj name: {IKartObj.name}");
            //    affectedKart.ReactToExplosion(force, transform.position, radius * 1.5f);
            //}

            // For kart trigger
            if (collider.TryGetComponent(out IKartTrigger affectedTrigger))
            {
                KartTrigger trigger = (KartTrigger)affectedTrigger;
                IKart iKart = trigger.Kart.GetComponent<IKart>();
                //ArcadeKart IKartObj = (ArcadeKart) affectedKart;
                //if (kart == null || kart == IKartObj)
                //{
                //    //Destroy(gameObject);
                //    return;
                //}
                //KartTrigger IKartTrig = (KartTrigger)affectedTrigger;

                if (this.trigger == null || this.trigger == trigger)
                {
                    //Destroy(gameObject);
                    return;
                }

                //Debug.Log($"Trigger name: {trigger.name}");
                //ArcadeKart affectedKartObj = (ArcadeKart)affectedKart;
                //affectedKart.ReactToExplosion(force, transform.position, radius * 1.5f);
                iKart.ReactToExplosion(force, transform.position, radius * 1.5f);
            }
        }

        //if (collision.gameObject.TryGetComponent(out IKart IKart))
        //{
        //    ArcadeKart IKartObj = (ArcadeKart)IKart;
        //    if (kart == null || kart == IKartObj)
        //    {
        //        //Destroy(gameObject);
        //        return;
        //    }

        //    //IKart.ReactToExplosion(force, transform.position, radius);

        //    Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        //    Debug.Log(colliders.Length);
        //    foreach (Collider collider in colliders)
        //    {
        //        if (collider.TryGetComponent(out IKart affectedKart))
        //        {
        //            //ArcadeKart affectedKartObj = (ArcadeKart)affectedKart;
        //            affectedKart.ReactToExplosion(force, transform.position, radius * 1.5f);
        //        }
        //    }
        //    //Debug.Log(kart.name);

        //}
        if (_explosion != null)
        {
            _explosion.gameObject.SetActive(true);
        }
        Instantiate(_explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
