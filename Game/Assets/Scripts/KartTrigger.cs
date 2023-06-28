using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartTrigger : MonoBehaviour, IKartTrigger
{
    public Player Player;

    // Start is called before the first frame update
    void Start()
    {
        if (Player == null) Player = GetComponentInParent<Player>();
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
