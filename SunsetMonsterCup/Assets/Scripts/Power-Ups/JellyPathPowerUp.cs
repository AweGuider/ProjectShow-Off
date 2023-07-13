using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class JellyPathPowerUp : ActivatableArcadeKartPowerup
{
    [SerializeField]
    private GameObject _trailPrefab;
    [SerializeField]
    private GameObject _trailObj;
    [SerializeField]
    private List<GameObject> _spawnedTrailSegments = new();

    [SerializeField]
    private Transform _trailsParent;

    [SerializeField]
    private int _maxTrailSegments = 10;

    public float spawnInterval = 0.2f;
    public float trailSegmentSpacing = 1f;

    private void OnEnable()
    {
        onPowerupActivated.AddListener(ActivateTrailSegment);
        onPowerupFinished.AddListener(DeactivateTrailSegment);
    }

    private void OnDisable()
    {
        onPowerupActivated.RemoveListener(ActivateTrailSegment);
        onPowerupFinished.RemoveListener(DeactivateTrailSegment);

    }

    public override void OnPowerUp(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            /// Don't change this part
            base.OnPowerUp(context);
            if (IsCoolingDown) return;

            ApplyPowerUps(kart);
            /// Don't change this part

            //ActivateTrailSegment();

            //StartCoroutine(SpawnTrailSegmentCoroutine());

            /// Write above

        }
    }


    void ActivateTrailSegment()
    {

        // Position the trail segment behind the kart
        Vector3 spawnPosition = kart.transform.position - kart.transform.forward * trailSegmentSpacing + new Vector3(0, 3f, -5f);
        Quaternion spawnRotation = kart.transform.rotation;

        _trailObj.transform.position = spawnPosition;
        _trailObj.transform.rotation = spawnRotation;

        // For now use SetActive
        _trailObj.SetActive(true);
    }

    void DeactivateTrailSegment()
    {
        // For now use SetActive
        _trailObj.SetActive(false);
    }

    IEnumerator SpawnTrailSegmentCoroutine()
    {
        while (_spawnedTrailSegments.Count < _maxTrailSegments)
        {
            //GameObject trailSegment = trailObjectPool.GetPooledObject();
            // Position the trail segment behind the kart
            Vector3 spawnPosition = kart.transform.position - kart.transform.forward * trailSegmentSpacing + new Vector3(0, 1, 0);
            Quaternion spawnRotation = kart.transform.rotation;


            //GameObject trailSegment = Instantiate(_trailPrefab, transform.position, transform.rotation);
            GameObject trailSegment = Instantiate(_trailPrefab, spawnPosition, spawnRotation);

            trailSegment.transform.SetParent(_trailsParent);
            _spawnedTrailSegments.Add(trailSegment);

            ResizeColliderToTrailBounds(trailSegment);

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void ResizeColliderToTrailBounds(GameObject trailSegment)
    {
        TrailRenderer trailRenderer = trailSegment.GetComponentInChildren<TrailRenderer>();

        // Calculate the bounds of the trail segment
        Bounds bounds = new Bounds(trailRenderer.transform.position, Vector3.zero);

        Vector3[] positions = new Vector3[trailRenderer.positionCount];
        trailRenderer.GetPositions(positions);
        foreach (Vector3 position in positions)
        {
            bounds.Encapsulate(position);
        }

        // Resize the collider to match the bounds
        BoxCollider collider = trailSegment.GetComponent<BoxCollider>();
        collider.size = bounds.size;
        collider.center = bounds.center - trailRenderer.transform.position;
    }
}
