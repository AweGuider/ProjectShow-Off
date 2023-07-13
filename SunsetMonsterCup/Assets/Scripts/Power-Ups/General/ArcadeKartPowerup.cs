using KartGame.KartSystems;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class ArcadeKartPowerup : MonoBehaviour {

    public ArcadeKart.StatPowerup boostStats = new ArcadeKart.StatPowerup
    {
        MaxTime = 5
    };

    public bool IsCoolingDown { get; protected set; }
    public bool IsGoing { get; protected set; }
    public float LastActivatedTimestamp { get; protected set; }

    public float cooldown = 5f;

    public bool disableGameObjectWhenActivated;
    public UnityEvent onPowerupActivated;
    public UnityEvent onPowerupFinished;
    public UnityEvent onPowerupFinishCooldown;

    private void Awake()
    {
        LastActivatedTimestamp = -9999f;
    }

    private void Update()
    {
        //if (isCoolingDown) { 

        //    if (Time.time - lastActivatedTimestamp > cooldown) {
        //        //finished cooldown!
        //        isCoolingDown = false;
        //        onPowerupFinishCooldown?.Invoke();
        //        // Think of how to properly see when cooldown finished visually
        //    }
        //}
    }

    //private void FixedUpdate()
    //{
    //    isGoing = boostStats.ElapsedTime < boostStats.MaxTime;
    //}

    protected void ApplyPowerUps(ArcadeKart kart)
    {
        boostStats.ElapsedTime = 0f;

        //lastActivatedTimestamp = Time.time;
        kart.AddPowerup(this.boostStats);

        StartCoroutine(InProcess());

        StartCoroutine(OnCooldown());

        if (disableGameObjectWhenActivated) this.gameObject.SetActive(false);
    }

    protected IEnumerator OnCooldown()
    {
        onPowerupActivated?.Invoke();
        IsCoolingDown = true;
        yield return new WaitForSeconds(cooldown);
        IsCoolingDown = false;
        onPowerupFinishCooldown?.Invoke();

    }

    protected IEnumerator InProcess()
    {
        IsGoing = true;
        yield return new WaitForSeconds(boostStats.MaxTime);
        IsGoing = false;
        onPowerupFinished?.Invoke();
    }

    //protected void ActivatePowerUps()
    //{
    //    lastActivatedTimestamp = Time.time;
    //    onPowerupActivated?.Invoke();
    //    isCoolingDown = true;

    //    if (disableGameObjectWhenActivated) this.gameObject.SetActive(false);
    //}
}
