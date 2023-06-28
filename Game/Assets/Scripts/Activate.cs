using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activate : MonoBehaviour
{
    [SerializeField] bool activate = true;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(activate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
