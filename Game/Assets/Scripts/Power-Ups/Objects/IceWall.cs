using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceWall : MonoBehaviour
{
    [SerializeField]
    private float _startingTime;

    public float ElapsedTime;

    //[SerializeField]
    //private float _elapsedTime;
    // Start is called before the first frame update
    void Start()
    {
        ElapsedTime = _startingTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ElapsedTime -= Time.fixedDeltaTime;
        if (ElapsedTime < 0)
        {
            Destroy(gameObject);
        }
    }
}
