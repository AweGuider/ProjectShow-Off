using KartGame.KartSystems;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public abstract class ActivatableArcadeKartPowerup : ArcadeKartPowerup
{
    [SerializeField]
    protected ArcadeKart kart;

    [SerializeField]
    protected Transform particlesParent;
    [SerializeField] private bool particlesNotEmpty;
    private void Start()
    {
        if (kart == null) kart = GetComponent<ArcadeKart>();
        //Debug.Log($"Kart name: {kart.name}");
    }

    public virtual void OnPowerUp(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //Debug.Log($"OnPowerUp clicked: {context.performed}");
            if (particlesNotEmpty)
            {
                foreach (ParticleSystem particle in particlesParent.GetComponentsInChildren<ParticleSystem>())
                {
                    particle.Play();
                }
            }

            /*if (IsCoolingDown)*/
                //Debug.Log($"Can activate powerup? {!IsCoolingDown}");
        }
    }

    public ArcadeKart GetKart()
    {
        return kart;
    }

    private void OnValidate()
    {
        particlesNotEmpty = particlesParent.childCount > 0 && particlesParent.GetComponentsInChildren<ParticleSystem>().Length > 0;
    }
}
