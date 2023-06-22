using KartGame.KartSystems;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FireBulletPowerUp : ActivatableArcadeKartPowerup
{
    public ArcadeKart.StatPowerup bumpStats = new ArcadeKart.StatPowerup();


    [SerializeField]
    private KnockbackZone _zone;

    [SerializeField]
    int _radius;

    public int Radius { get => _radius; }



    private void OnCollisionEnter(Collision collision)
    {
        if (IsGoing)
        {
            if (collision.gameObject.TryGetComponent(out IKart kart))
            {
                kart.ReactToBump(bumpStats);

            }
        }

    }


    public override void OnPowerUp(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            /// Don't change this part
            base.OnPowerUp(context);
            if (IsCoolingDown) return;

            ApplyPowerUps(kart);
            /// Don't change this part



            /// Write above

        }
    }
}
