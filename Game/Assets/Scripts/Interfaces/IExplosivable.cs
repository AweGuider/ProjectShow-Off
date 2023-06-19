using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IExplosivable
{
    void ReactToExplosion(float force, Vector3 position, float radius);
}
