using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KartGame.KartSystems;

public interface IPlayer
{
    void SetCheckpoint(Checkpoint cp);
    void ReactToDead();
}
