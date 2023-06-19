using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]

public class TestObject : MonoBehaviour, IExplosivable
{
    public void ReactToExplosion(float force, Vector3 position, float radius)
    {
        Debug.Log($"{name} got hit by explosion");
        GetComponent<Rigidbody>().AddExplosionForce(force, position, radius);

    }
}
