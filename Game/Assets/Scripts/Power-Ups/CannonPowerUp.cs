using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CannonPowerUp : ActivatableArcadeKartPowerup
{

    public override void OnPowerUp(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            /// Don't change this part
            base.OnPowerUp(context);
            if (isCoolingDown) return;
            ApplyPowerUps(kart);
            /// Till here

            /// Write below

        }
    }
}
