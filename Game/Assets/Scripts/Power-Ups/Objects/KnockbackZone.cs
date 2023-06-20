using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class KnockbackZone : MonoBehaviour
{
    private NestedPowerUp _powerUp;

    private List<GameObject> _nearby;
    public List<GameObject> Nearby { get => _nearby; }

    public SphereCollider sphere;

    [SerializeField]
    private float _startingTime;

    public float ElapsedTime;

    //[SerializeField]
    //private float _elapsedTime;
    // Start is called before the first frame update

    private void Awake()
    {
        _nearby = new();
        if (_powerUp == null) _powerUp = GetComponentInParent<NestedPowerUp>();

        sphere = GetComponent<SphereCollider>();
        sphere.radius = _powerUp.Radius;
    }
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
            //Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.transform.TryGetComponent(out IExplosivable explosivable))
        {
            _nearby.Add(other.gameObject);
        }

        Debug.Log($"Nearby Count Now: {Nearby.Count}");
    }

    private void OnTriggerExit(Collider other)
    {
        if (_nearby.Contains(other.gameObject))
            _nearby.Remove(other.gameObject);
        Debug.Log($"Nearby Count Left: {Nearby.Count}");

    }
}