using KartGame.KartSystems;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public abstract class ActivatableArcadeKartPowerup : ArcadeKartPowerup
{
    [SerializeField]
    protected ArcadeKart kart;

    private void Start()
    {
        if (kart == null) kart = GetComponent<ArcadeKart>();
        Debug.Log($"Kart name: {kart.name}");
    }

    public virtual void OnPowerUp(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log($"Activated PowerUp: {context.performed}");
        }
    }
}
