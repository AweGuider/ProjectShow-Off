using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KartGame.KartSystems;

public interface IKart
{
    void ReactToExplosion(float force, Vector3 position, float radius);
    void ReactToBump(ArcadeKart.StatPowerup debuff);
    void ReactToJellyPath(ArcadeKart.StatPowerup debuff);
}
