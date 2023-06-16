using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NestedPowerUp : ActivatableArcadeKartPowerup
{

    [SerializeField]
    private Cannon _cannon;

    public float FirePowerMultiplier = 1f;

    event Action<float, float> Fire;

    private void Awake()
    {
        if (_cannon == null) _cannon = gameObject.GetComponentInChildren<Cannon>();
    }

    private void OnEnable()
    {
        Fire += _cannon.FireTheBall;
    }

    private void OnDisable()
    {
        Fire -= _cannon.FireTheBall;
    }

    public override void OnPowerUp(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            /// Don't change this part
            base.OnPowerUp(context);
            if (isCoolingDown) return;
            ApplyPowerUps(kart);
            /// Don't change this part

            Fire?.Invoke(kart.Rigidbody.velocity.magnitude, FirePowerMultiplier);

            /// Write below

        }
    }
}
