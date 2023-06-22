using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailSegment : MonoBehaviour
{


    private void Awake()
    {
        gameObject.SetActive(false);
    }
    private void Update()
    {
        //ResizeColliderToTrailBounds();
    }

    private void ResizeColliderToTrailBounds()
    {
        TrailRenderer trailRenderer = GetComponent<TrailRenderer>();

        // Calculate the bounds of the trail segment
        Bounds bounds = new Bounds(trailRenderer.transform.position, Vector3.zero);

        Vector3[] positions = new Vector3[trailRenderer.positionCount];
        trailRenderer.GetPositions(positions);
        foreach (Vector3 position in positions)
        {
            bounds.Encapsulate(position);
        }

        // Resize the collider to match the bounds
        BoxCollider collider = GetComponent<BoxCollider>();
        collider.size = bounds.size;
        collider.center = bounds.center - trailRenderer.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Implement trigger behavior here (e.g., apply a debuff to other karts)
        // You can access the other object using 'other.gameObject'
        if (other.gameObject.TryGetComponent(out IKart kart) && other.gameObject != transform.parent.gameObject)
        {
            Debug.Log($"TRIGGERED {other.name}");

        }
    }
}
