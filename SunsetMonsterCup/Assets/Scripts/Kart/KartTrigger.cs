using KartGame.KartSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartTrigger : MonoBehaviour, IKartTrigger
{
    public Player Player;
    public ArcadeKart Kart;

    // Start is called before the first frame update
    void Start()
    {
        if (Player == null) Player = GetComponentInParent<Player>();
        if (Kart == null) Kart = GetComponentInParent<ArcadeKart>();
    }
    public void SetCheckpoint(Checkpoint cp)
    {
        if (Player.Checkpoint != cp) Player.Checkpoint = cp;

    }
    public void Respawn()
    {
        Player.Respawn();
    }

    //public 
}
