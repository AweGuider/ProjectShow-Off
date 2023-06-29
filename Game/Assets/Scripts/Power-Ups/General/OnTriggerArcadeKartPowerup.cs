using KartGame.KartSystems;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerArcadeKartPowerup : ArcadeKartPowerup
{
    private void OnTriggerEnter(Collider other)
    {
        if (IsCoolingDown) return;

        if (other.gameObject.TryGetComponent(out IKartTrigger kartTrigger))
        {
            KartTrigger trigger = (KartTrigger) kartTrigger;
            ApplyPowerUps(trigger.Kart);
            Debug.Log($"TRIGGERED {other.name}");

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
