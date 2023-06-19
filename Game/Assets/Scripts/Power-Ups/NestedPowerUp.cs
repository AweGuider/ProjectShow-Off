using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NestedPowerUp : ActivatableArcadeKartPowerup
{
    [SerializeField]
    private KnockbackZone _zone;

    [SerializeField]
    int _radius;

    public int Radius { get => _radius; }

    [SerializeField]
    int _force;


    event Action<float, float> Fire;

    private void Awake()
    {

    }

    //private void OnEnable()
    //{
    //    Fire += _cannon.FireTheBall;
    //}

    //private void OnDisable()
    //{
    //    Fire -= _cannon.FireTheBall;
    //}

    public override void OnPowerUp(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            /// Don't change this part
            base.OnPowerUp(context);
            if (isCoolingDown) return;
            ApplyPowerUps(kart);
            /// Don't change this part

            foreach (GameObject t in _zone.Nearby)
            {
                t.GetComponent<IExplosivable>().ReactToExplosion(_force, _zone.gameObject.transform.position, _radius);
            }

            /// Write above

        }
    }
}
