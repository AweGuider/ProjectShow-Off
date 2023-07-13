using KartGame.KartSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]

public class TestObject : MonoBehaviour, IKart, IKartTrigger
{
    public void ReactToBump(ArcadeKart.StatPowerup debuff)
    {
        throw new System.NotImplementedException();
    }

    public void ReactToJellyPath(ArcadeKart.StatPowerup debuff)
    {
        throw new System.NotImplementedException();

    }

    public void ReactToExplosion(float force, Vector3 position, float radius)
    {
        //Debug.Log($"{name} got hit by explosion");
        GetComponent<Rigidbody>().AddExplosionForce(force, position, radius);

    }

    public void SetCheckpoint(Checkpoint cp)
    {
        // Set checkpoint position

    }
    public void Respawn()
    {
        // Reset position to saved checkpoint
        // (Additional) Set velocity/speed to zero so the player doesnt respawn with super high speed

    }
}
