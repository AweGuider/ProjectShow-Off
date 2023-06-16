using KartGame.KartSystems;
using UnityEngine;
using UnityEngine.Events;

public abstract class ArcadeKartPowerup : MonoBehaviour {

    public ArcadeKart.StatPowerup boostStats = new ArcadeKart.StatPowerup
    {
        MaxTime = 5
    };

    public bool isCoolingDown { get; protected set; }
    public float lastActivatedTimestamp { get; protected set; }

    public float cooldown = 5f;

    public bool disableGameObjectWhenActivated;
    public UnityEvent onPowerupActivated;
    public UnityEvent onPowerupFinishCooldown;

    private void Awake()
    {
        lastActivatedTimestamp = -9999f;
    }

    private void Update()
    {
        if (isCoolingDown) { 

            if (Time.time - lastActivatedTimestamp > cooldown) {
                //finished cooldown!
                isCoolingDown = false;
                onPowerupFinishCooldown?.Invoke();
                // Think of how to properly see when cooldown finished visually
                //boostStats.ElapsedTime = 0f;
            }
        }
    }

    protected void ApplyPowerUps(ArcadeKart kart)
    {
        lastActivatedTimestamp = Time.time;
        kart.AddPowerup(this.boostStats);
        onPowerupActivated?.Invoke();
        isCoolingDown = true;

        if (disableGameObjectWhenActivated) this.gameObject.SetActive(false);
    }

    //protected void ActivatePowerUps()
    //{
    //    lastActivatedTimestamp = Time.time;
    //    onPowerupActivated?.Invoke();
    //    isCoolingDown = true;

    //    if (disableGameObjectWhenActivated) this.gameObject.SetActive(false);
    //}
}
