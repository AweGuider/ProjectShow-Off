using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KartGame.KartSystems;

public interface IKartTrigger
{
    void SetCheckpoint(Checkpoint cp);
    void Respawn();
}
