using KartGame.KartSystems;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerArcadeKartPowerup : ArcadeKartPowerup
{
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log($"COLLIDED {other.gameObject.name}");

        if (IsCoolingDown) return;

        if (other.gameObject.TryGetComponent(out IKart kartTrigger))
        {
            KartTrigger trigger = (KartTrigger) kartTrigger;
            kartTrigger.ReactToJellyPath(boostStats);
            //ApplyPowerUps(trigger.Kart);
            Debug.Log($"COLLIDED {other.gameObject.name}");

        }

        //var rb = other.attachedRigidbody;

        //Debug.Log($"Rigibody name: {rb.name}");
        //if (rb) {

        //    var kart = rb.GetComponent<ArcadeKart>();
        //    Debug.Log($"Kart name: {kart.name}");

        //    if (kart)
        //    {
        //        ApplyPowerUps(kart);
        //    }
        //}
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"TRIGGERED {other.gameObject.name}");

        if (IsCoolingDown) return;

        if (other.gameObject.TryGetComponent(out IKart kartTrigger))
        {
            KartTrigger trigger = (KartTrigger) kartTrigger;
            kartTrigger.ReactToJellyPath(boostStats);
            //ApplyPowerUps(trigger.Kart);
            Debug.Log($"TRIGGERED {other.gameObject.name}");

        }

        //var rb = other.attachedRigidbody;

        //Debug.Log($"Rigibody name: {rb.name}");
        //if (rb) {

        //    var kart = rb.GetComponent<ArcadeKart>();
        //    Debug.Log($"Kart name: {kart.name}");

        //    if (kart)
        //    {
        //        ApplyPowerUps(kart);
        //    }
        //}
    }
}
